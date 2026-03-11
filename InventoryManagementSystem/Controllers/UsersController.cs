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

        public UsersController(IUserService userService, UserManager userManager)
        {
            _userService = userService;
            _userManager = userManager;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _userService.IsInRoleAdmin(userId))
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
            return BadRequest(new { Success = false, Message = "Not enough privileges to view this page" } );
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            var result = await _userManager.GetUserInfo(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Success = false, Message = "User not found" });
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoViewModel userInfo)
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetAccessForInventory(int inventoryId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var hasAccess = await _userService.GetAccessInventory(userId, inventoryId);

            if (hasAccess)
            {
                return Ok(hasAccess);
            }
            else
            {
                return BadRequest();
            }
        }
    }

}
