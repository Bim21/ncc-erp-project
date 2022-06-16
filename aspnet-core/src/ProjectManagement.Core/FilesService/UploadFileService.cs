
using Abp.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public class UploadFileService: ITransientDependency
    {
       
        private readonly ILogger<UploadFileService> _logger;
        private readonly IFileService _fileService;
        public UploadFileService(IFileService fileService, ILogger<UploadFileService> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        public Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadAvatarAsync(IFormFile file)
        {
            return _fileService.UploadAvatarAsync(file);
        }
    }
}
