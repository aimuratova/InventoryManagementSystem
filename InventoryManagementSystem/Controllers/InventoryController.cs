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
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int inventoryId)
        {
            var result = await _inventoryManager.Delete(inventoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]InventoryItemViewModel inventoryModel)
        {
            var result = await _inventoryManager.Add(inventoryModel);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
