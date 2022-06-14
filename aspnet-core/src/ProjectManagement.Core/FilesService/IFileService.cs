using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public interface IFileService
    {
        Task<string> UploadImageFileAsync(IFormFile file, long userId);

        Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, long userId);
    }
}
