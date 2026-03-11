using AutoMapper;
using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Managers
{
    public class UserManager
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IInventoryUserService _inventoryUserService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IMapper _mapper;

        public UserManager(IUserService userService, IRoleService roleService, IInventoryService inventoryService, IInventoryUserService inventoryUserService, 
            IDictionaryService dictionaryService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _inventoryUserService = inventoryUserService;
            _dictionaryService = dictionaryService;
            _mapper = mapper;
        }

        public async Task<ResultModel> UpdateUserAsync(UserInfoViewModel user)
        {
            try
            {
                var updateUserResult = await _userService.UpdateUserAsync(new DAL.Models.UsersModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                });

                if (updateUserResult == null || !updateUserResult.Success)
                {
                    throw new Exception(updateUserResult?.Message ?? "Failed to update user.");
                }

                updateUserResult = await _roleService.AddUserToRoleAdminAsync(user.Id, user.IsAdmin);

                if (updateUserResult == null || !updateUserResult.Success)
                {
                    throw new Exception(updateUserResult?.Message ?? "Failed to update user roles.");
                }

                updateUserResult = await _inventoryUserService.AddInventoryItemUser(user.Id, user.Inventories?.Select(x => x.Key).ToList());

                if (updateUserResult == null || !updateUserResult.Success)
                {
                    throw new Exception(updateUserResult?.Message ?? "Failed to update user inventory access.");
                }

                return updateUserResult;
            }
            catch (Exception ex)
            {
                return new ResultModel()
                {
                    Success = false,
                    Message = "An error occurred while updating the user.",
                    Errors = new List<string>() { ex.Message }
                };
            }
        }

        public async Task<ResultModel<List<UserViewModel>>> ListUsersAsync()
        {
            var resultList = await _userService.ListUsers();
            if (resultList.Success)
            {
                var inventoriesList = await _inventoryUserService.GetInventoryItemsUserModels();

                var list = _mapper.Map<List<UserViewModel>>(resultList.Data);
                list.ForEach(x => x.Inventories =
                    string.Join(", ", inventoriesList.Where(i => i.UserId == x.Id).Select(i => i.InventoryItemTitle)));

                return new ResultModel<List<UserViewModel>>()
                {
                    Success = true,
                    Data = list
                };
            }
            else
            {
                return new ResultModel<List<UserViewModel>>()
                {
                    Success = false,
                    Message = resultList.Message
                };
            }

        }

        public async Task<UserInfoViewModel?> GetUserInfo(string userId)
        {
            var result = await _userService.GetUserById(userId);
            var resultInventories = await _dictionaryService.GetInventoriesAsync();
            var selectedInventories = await _inventoryUserService.GetInventoryItemsUserModels(userId);

            if (result.Success)
            {
                var userInfo = _mapper.Map<UserInfoViewModel>(result.Data);
                userInfo.Inventories = resultInventories;
                userInfo.SelectedInventories = selectedInventories.ToDictionary(i => i.InventoryItemId, i => i.InventoryItemTitle);
                
                return userInfo;
            }
            return null;
        }
    }
}
