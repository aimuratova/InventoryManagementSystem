using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Managers
{
    public class UserManager
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryUserService _inventoryUserService;

        public UserManager(IUserService userService, IRoleService roleService, IInventoryService inventoryService, IInventoryUserService inventoryUserService)
        {
            _userService = userService;
            _roleService = roleService;
            _inventoryService = inventoryService;
            _inventoryUserService = inventoryUserService;
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

                var list = resultList.Data?
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        EmailConfirmed = x.EmailConfirmed ?? false,
                        UserName = x.UserName,
                        IsAdmin = x.IsAdmin,
                        Inventories = string.Join(", ", inventoriesList?.Where(i => i.UserId == x.Id).Select(i => i.InventoryItemTitle) ?? new List<string>())
                    }).ToList();

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
    }
}
