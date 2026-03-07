using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryValueController : Controller
    {
        private readonly InventoryValueManager _inventoryValueManager;

        public InventoryValueController(InventoryValueManager inventoryValueManager)
        {
            _inventoryValueManager = inventoryValueManager;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<ValueViewModel> values)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _inventoryValueManager.AddValue(values, userId);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, int inventoryId)
        {
            var model = await _inventoryValueManager.GetInventoryValueInfo(id, inventoryId);
            return View(model);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetAccessForValue(int valueId)
        {
            // TODO: implement logic here
            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, int inventoryId)
        {
            var model = await _inventoryValueManager.GetInventoryValueInfo(id, inventoryId);
            return View(model);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int valueId)
        {
            //var result = await _inventoryValueManager.Delete(inventoryId);
            //if (result.Success)
            //{
            //    return Ok(result);
            //}
            //else
            //{
            //    return BadRequest(result.Message);
            //}
            return Ok(new { Success = true });
        }
    }
}
