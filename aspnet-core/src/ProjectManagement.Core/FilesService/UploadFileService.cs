using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public class UploadFileService : IFileService
    {
        private readonly AmazonS3Service _amazonS3Service;
        private readonly InternalUploadFileService _internalUploadFileService;
        private readonly ILogger<UploadFileService> _logger;
        public UploadFileService(HttpClient httpClient, AmazonS3Service amazonS3Service, InternalUploadFileService internalUploadFileService, ILogger<UploadFileService> logger)
        {
            _amazonS3Service = amazonS3Service;
            _internalUploadFileService = internalUploadFileService;
            _logger = logger;
        }

        public Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadImageFileAsync(IFormFile file, long userId)
        {
            if (Constants.ConstantUploadFile.Provider == Constants.ConstantUploadFile.AmazoneS3)
            {
                return _amazonS3Service.UploadImageFileAsync(file, userId);
            }
            else
            {
                return _internalUploadFileService.UploadImageFileAsync(file, userId);
            }
        }
    }
}
