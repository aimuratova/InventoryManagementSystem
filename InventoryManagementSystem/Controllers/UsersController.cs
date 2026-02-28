using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager _userManager;
        private readonly IDictionaryService _dictionaryService;
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryUserService _inventoryUserService;

        public UsersController(IUserService userService, IDictionaryService dictionaryService, UserManager userManager,
            IInventoryService inventoryService)
        {
            _userService = userService;
            _dictionaryService = dictionaryService;
            _userManager = userManager;
            _inventoryService = inventoryService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _userService.GetUserById(userId);
                if (result.Success)
                {
                    return Ok(new { user = result.Data });
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(new { message = "User ID not found in token." });
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var resultList = await _userManager.ListUsersAsync();
            if (resultList.Success)
            {               
                return Ok(resultList.Data);
            }
            else
            {
                return BadRequest(resultList);
            }

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            var result = await _userService.GetUserById(id);
            var resultInventories = await _dictionaryService.GetInventoriesAsync();
            var selectedInventories = await _inventoryUserService.GetInventoryItemsUserModels(id);

            if (result.Success)
            {
                var userInfo = result.Data;
                var userInfoViewModel = new UserInfoViewModel
                {
                    Id = userInfo.Id,
                    Email = userInfo.Email,
                    EmailConfirmed = userInfo.EmailConfirmed ?? false,
                    UserName = userInfo.UserName,
                    IsAdmin = userInfo.IsAdmin,
                    Inventories = resultInventories,
                    SelectedInventories = selectedInventories.ToDictionary(i => i.InventoryItemId, i => i.InventoryItemTitle)
                };
                return Ok(userInfoViewModel);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo([FromBody]UserInfoViewModel userInfo)
        {
            var result = await _userManager.UpdateUserAsync(userInfo);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }

}
