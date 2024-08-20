using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    }
     
    public class CloudinaryService : ICloudinaryService
    {

        private readonly Cloudinary _cloudinary;
        private readonly long _maxFileSizeInBytes = 524288000000;
        private readonly long _minFileSizeInBytes = 512;
        private readonly string[] _allowedFileTypes = { "image/jpeg", "image/png", "image/gif" };

        public CloudinaryService(Cloudinary cloudinary) => _cloudinary = cloudinary;

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            // Validate file size
            if (file.Length < _minFileSizeInBytes)
            {
                uploadResult.Error = new Error { Message = "File is too small." };
                return uploadResult;
            }
            if (file.Length > _maxFileSizeInBytes)
            {
                uploadResult.Error = new Error { Message = "File is too large." };
                return uploadResult;
            }

            // Validate file type
            if (!_allowedFileTypes.Contains(file.ContentType))
            {
                uploadResult.Error = new Error { Message = "Invalid file type." };
                return uploadResult;
            }

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }

    }
}
