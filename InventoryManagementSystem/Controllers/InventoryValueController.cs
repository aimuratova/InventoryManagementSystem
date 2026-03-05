using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryValueController : Controller
    {
        private readonly InventoryManager _inventoryManager;

        public InventoryValueController(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;            
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<ValueViewModel> values)
        {
            var result = await _inventoryManager.AddValue(values);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }
    }
}
