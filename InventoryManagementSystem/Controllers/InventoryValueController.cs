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
    }
}
