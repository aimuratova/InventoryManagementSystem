using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
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

        public async Task<IActionResult> Index()
        {
            var resultList = await _inventoryManager.GetAllItemsAsync();
            if(resultList == null)
            {
                _logger.LogError("Failed to retrieve inventory items.");
                return View(new List<InventoryItemViewModel>());
            }

            return View(resultList);
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
