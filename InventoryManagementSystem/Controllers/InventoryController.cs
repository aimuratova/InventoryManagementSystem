using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryManager _inventoryManager;

        public InventoryController(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int inventoryId)
        {            
            var result = await _inventoryManager.GetInventoryInfoByIdAsync(inventoryId);
            return View(result);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Edit(int inventoryId)
        {
            var result = await _inventoryManager.GetInventoryInfoByIdForEdit(inventoryId);
            
            return View(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Save(InventoryEditViewModel inventory)
        {
            var result = await _inventoryManager.Save(inventory);

            return Ok(result);
        }
    }
}
