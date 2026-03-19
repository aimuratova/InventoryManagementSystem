using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Services;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace InventoryManagementSystem.Controllers
{
    public class ExternalServicesController : Controller
    {
        private readonly IExternalSyncService _externalSyncService;

        public ExternalServicesController(IExternalSyncService externalSyncService)
        {
            _externalSyncService = externalSyncService;
        }

        [HttpGet]
        public IActionResult SyncToSf()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> SyncToSf([FromBody] SendToSalesforceModel sendToSalesforceModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _externalSyncService.SyncToSalesForce(userId, sendToSalesforceModel.Company, sendToSalesforceModel.Phone);

            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }
    }
}
