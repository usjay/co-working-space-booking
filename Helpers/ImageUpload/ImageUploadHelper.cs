using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace coreworking_space_booking_backend.Helpers.ImageUpload
{
    public class ImageUploadHelper
    {
        private readonly IConfiguration _configuration;

        public ImageUploadHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SaveFile(IFormFile file, string type)
        {
            if (file == null)
                throw new ArgumentException("File is null");

            
            string folderPath = type switch
            {
                "UserAvatar" => _configuration["AvatarSettings:UserAvatarFolder"],
                "AdminAvatar" => _configuration["AvatarSettings:AdminAvatarFolder"],
                "ProductImage" => _configuration["AvatarSettings:ProductImageFolder"],
                _ => throw new ArgumentException("Invalid type")
            };

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            
            return fullPath.Replace("wwwroot", "").Replace("\\", "/");
        }
    }
}
