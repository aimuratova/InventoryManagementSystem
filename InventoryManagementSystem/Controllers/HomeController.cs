using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InventoryManager _inventoryManager;

        public HomeController(ILogger<HomeController> logger, InventoryManager inventoryManager)
        {
            _logger = logger;
            _inventoryManager = inventoryManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ListAll([FromQuery] ListQueryParam queryParam)
        {
            var resultList = await _inventoryManager.GetAllItemsAsync(categoryId: queryParam.CategoryId, searchText: queryParam.SearchText);
            
            return Ok(resultList);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> ListAuth([FromQuery] ListQueryParam queryParam)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _inventoryManager.GetAllItemsAsync(userId: userId, 
                    categoryId: queryParam.CategoryId, searchText: queryParam.SearchText, inventoryType: queryParam.InventoryType);
                return Ok(result);
            }

            return BadRequest("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
