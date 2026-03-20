using AutoMapper;
using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    public class SupportController : Controller
    {
        private readonly ISupportService _supportService;
        private readonly IMapper _mapper;

        public SupportController(ISupportService supportService, IMapper mapper)
        {
            _supportService = supportService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupportTicketViewModel supportTicketModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            supportTicketModel.ReportedBy = userId;
            supportTicketModel.CreatedAt = DateTime.UtcNow;

            var ticketModel = _mapper.Map<SupportTicketModel>(supportTicketModel);

            var result = await _supportService.Create(ticketModel);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }
    }
}
