using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IDictionaryRepository _dictionaryRepository;

        public RoleService(IUserRepository userRepository, IRoleRepository roleRepository, IDictionaryRepository dictionaryRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<ResultModel> AddUserToRoleAdminAsync(string userId, bool isAdmin)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user != null && !string.IsNullOrEmpty(user.Id))
            {
                if(user.IsAdmin != isAdmin)
                {
                    var adminRole = await _dictionaryRepository.GetRolesAsync("Admin");

                    if (isAdmin)
                    {
                        user.IsAdmin = true;
                        await _roleRepository.AddUserAdminRole(user.Id, adminRole.FirstOrDefault().Key);
                        return new ResultModel { Success = true, Message = "User has been added to the admin role." };
                    }
                    else
                    {
                        user.IsAdmin = false;
                        await _roleRepository.RemoveUserAdminRole(user.Id, adminRole.FirstOrDefault().Key);
                        return new ResultModel { Success = true, Message = "User has been removed from the admin role." };
                    }

                }
                else
                {
                    return new ResultModel { Success = true, Message = "User already has the specified role." };
                }
            }
            return new ResultModel { Success = false, Message = "User not found." };
        }
    }
}
