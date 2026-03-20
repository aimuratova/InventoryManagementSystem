using CloudinaryDotNet;
using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace InventoryManagementSystem.BLL.Services
{
    public class SupportService : ISupportService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly ITokenAcquisition _tokenAcquisition;
        //private readonly string[] _scopes;

        public SupportService(IConfiguration configuration, IHttpClientFactory httpClientFactory)//, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            //_tokenAcquisition = tokenAcquisition;
            //_scopes = configuration["AzureAd:Scopes"].Split(' ');
        }

        public async Task<ResultModel> Create(SupportTicketModel supportTicketModel)
        {
            var result = new ResultModel();

            try
            {
                if (string.IsNullOrEmpty(supportTicketModel.Link))
                {
                    result.Success = false;
                    result.Message = "Link was not provided";
                }
                else
                {
                    string fileName = $"ticket-{DateTime.UtcNow.Ticks}.json";
                    string json = JsonSerializer.Serialize(supportTicketModel, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    await UploadToOneDrive(json, fileName);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        private async Task UploadToOneDrive(string json, string fileName)
        {            
            var client = _httpClientFactory.CreateClient("Graph");
            var token = await GetAccessToken();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Для конкретного пользователя OneDrive
            var userId = _configuration["OneDrive:ObjectId"]; // "user@domain.com"; // email или objectId
            var response = await client.PutAsync($"users/{userId}/drive/root:/{fileName}:/content", content);

            var errorBody = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
        }

        private async Task<string?> GetAccessToken()
        {
            var _app = ConfidentialClientApplicationBuilder
                .Create(_configuration["OneDrive:ClientId"])
                .WithClientSecret(_configuration["OneDrive:ClientSecret"])
                .WithRedirectUri(_configuration["OneDrive:RedirectUri"])
                .WithAuthority(AzureCloudInstance.AzurePublic, _configuration["OneDrive:TenantId"])
                .Build();

            var scopes = new string[] { "https://graph.microsoft.com/.default" };

            var result = await _app.AcquireTokenForClient(scopes)
                   .ExecuteAsync();

            return result.AccessToken;
        }
    }
}
