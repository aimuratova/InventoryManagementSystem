using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class CloudinaryUploaderService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;

        public CloudinaryUploaderService(IConfiguration configuration)
        {
            _configuration = configuration;

            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            Account account = new Account(cloudName, apiKey, apiSecret);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = Path.GetFileNameWithoutExtension(file.FileName)
            };

            ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }
    }
}
