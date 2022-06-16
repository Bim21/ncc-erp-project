using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public interface IFileService
    {
        Task<string> UploadAvatarAsync(IFormFile file);

        Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, string filePath);
    }
}
