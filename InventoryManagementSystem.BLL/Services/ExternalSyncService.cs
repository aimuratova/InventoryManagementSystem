using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class ExternalSyncService : IExternalSyncService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public ExternalSyncService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<ResultModel> SyncToSalesForce(string userId, string companyName, string phone)
        {
            var result = new ResultModel();
            try
            {
                var tokenResponse = await GetAccessToken();

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

                // 1. Create Account
                var accountPayload = new
                {
                    Name = companyName
                };

                var accountRes = await client.PostAsync(
                    $"{tokenResponse.InstanceUrl}/services/data/v60.0/sobjects/Account",
                    new StringContent(JsonConvert.SerializeObject(accountPayload), Encoding.UTF8, "application/json"));

                var accountJson = await accountRes.Content.ReadAsStringAsync();
                dynamic accountObj = JsonConvert.DeserializeObject(accountJson);

                string accountId = accountObj.id;

                var userDetails = await _userService.GetUserById(userId);

                // 2. Create Contact (linked)
                var contactPayload = new
                {
                    LastName = userDetails.Data.UserName,
                    Email = userDetails.Data.Email,
                    Phone = phone,
                    AccountId = accountId
                };

                var contactRes = await client.PostAsync(
                    $"{tokenResponse.InstanceUrl}/services/data/v60.0/sobjects/Contact",
                    new StringContent(JsonConvert.SerializeObject(contactPayload), Encoding.UTF8, "application/json"));

                result.Success = contactRes.IsSuccessStatusCode;

                if (!result.Success)
                {
                    result.Message = "Error occured";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        private async Task<TokenResponse> GetAccessToken()
        {
            var client = _httpClientFactory.CreateClient();

            var consumerKey = _configuration["Salesforce:ConsumerKey"];
            var username = _configuration["Salesforce:Username"];
            var certPath = _configuration["Salesforce:CertPath"];
            var certPassword = _configuration["Salesforce:CertPassword"];

            var certificate = new X509Certificate2(certPath, certPassword);

            var now = DateTime.UtcNow;

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = consumerKey,
                Audience = "https://login.salesforce.com",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", username)
                }),
                NotBefore = now,
                Expires = now.AddMinutes(3),
                SigningCredentials = new SigningCredentials(
                    new X509SecurityKey(certificate),
                    SecurityAlgorithms.RsaSha256)
            };

            var token = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(token);

            var response = await client.PostAsync(
                "https://login.salesforce.com/services/oauth2/token",
                new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string,string>(
                "grant_type",
                "urn:ietf:params:oauth:grant-type:jwt-bearer"),
            new KeyValuePair<string,string>("assertion", jwt)
                }));

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Salesforce Auth Error: {json}");

            return JsonConvert.DeserializeObject<TokenResponse>(json);
        }
    }
}
