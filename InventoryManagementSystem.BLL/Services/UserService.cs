using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ResultModel<string>> AuthenticateAsync(string email, string password)
        {
            var result = new ResultModel<string>();
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var user = await _userRepository.GetByEmail(email);

                    if (user == null || String.IsNullOrEmpty(user.Id))
                    {
                        throw new Exception("Failed to login, user not found");
                    }
                    
                    var resultPassword = _passwordHasher.Verify(password, user!.PasswordHash);

                    if (!resultPassword)
                    {
                        throw new Exception("Failed to login, wrong password");
                    }

                    var tokenStr = _jwtTokenService.GenerateToken(user);
                    result.Success = true;
                    result.Data = tokenStr;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = $"Failed to login user: {ex.Message}";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Failed to login user";
            }

            return result;
        }

        public async Task<ResultModel> CreateAsync(string email, string password)
        {
            var result = new ResultModel();

            try
            {
                var hashedPassword = _passwordHasher.Generate(password);
                string userId = Guid.NewGuid().ToString();

                var user = new UsersModel
                {
                    Id = userId,
                    Email = email,
                    PasswordHash = hashedPassword,
                    UserName = email
                };

                await _userRepository.Add(user);
                result.Success = true;
                //await _userService.SendConfirmationEmail(email, userId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to register user: {ex.Message}";
            }            

            return result;
        }

        public async Task<bool> GetAccessInventory(string? userId, int inventoryId)
        {
            return await _userRepository.GetAccessInventory(userId, inventoryId);
        }

        public async Task<ResultModel<UsersModel>> GetUserById(string userId)
        {
            var result = new ResultModel<UsersModel>();

            var user = await _userRepository.GetUserById(userId);

            if (user == null || String.IsNullOrEmpty(user.Id))
            {
                result.Success = false;
                result.Message = "User not found";
            }
            else
            {
                result.Success = true;
                result.Data = user;
            }
            return result;
        }

        public async Task<bool> IsInRoleAdmin(string? userId)
        {
            return await _userRepository.IsInRoleAdmin(userId);
        }

        public async Task<ResultModel<List<UsersModel>>> ListUsers()
        {
            var list = await _userRepository.GetUsers();            
            return new ResultModel<List<UsersModel>>() { Success = true, Data = list};
        }

        public async Task<ResultModel> UpdateUserAsync(UsersModel user)
        {
            var result = new ResultModel();
            try
            {
                await _userRepository.UpdateAsync(user);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to update user: {ex.Message}";
            }

            return result;
        }
    }
}
