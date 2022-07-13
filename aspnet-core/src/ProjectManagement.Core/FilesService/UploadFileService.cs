﻿
using Abp.Application.Services;
using Abp.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagement.MultiTenancy;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public class UploadFileService: ApplicationService, ITransientDependency
    {
       
        private readonly ILogger<UploadFileService> _logger;
        private readonly IFileService _fileService;
        private readonly TenantManager _tenantManager;
        public UploadFileService(
            IFileService fileService, 
            ILogger<UploadFileService> logger,
            TenantManager tenantManager)
        {
            _fileService = fileService;
            _logger = logger;
            _tenantManager = tenantManager;
        }

        public Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAvatarAsync(IFormFile file)
        {
            var filePath = await _fileService.UploadAvatarAsync(file);
            return filePath;
        }
        public async Task<string> UploadTimesheetFileAsync(IFormFile file, int year, int month, string filename)
        {
            var tenantName = getSessionTenantName();           
            var filePath = await _fileService.UploadTimsheetAsync(file, tenantName, year, month, filename);
            return filePath;
        }

        public async Task<byte[]> DownloadTimesheetFileAsync(string filePath)
        {
            return await _fileService.DownloadFileAsync(filePath);
        }

        private string getSessionTenantName()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return "host";
            }

            var tenant = _tenantManager.GetById(AbpSession.TenantId.Value);
            
            return tenant.TenancyName.ToLower();
        }
    }
}
