using Abp.UI;
using Microsoft.AspNetCore.Http;
using ProjectManagement.Constants;
using ProjectManagement.Helper;
using ProjectManagement.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public class InternalUploadFileService : IFileService
    {
        private readonly string WWWRootFolder = "wwwroot";

        private void CheckValidFile(IFormFile file, string[] allowFileTypes)
        {
            var fileExt = FileUtils.GetFileExtension(file);
            if (!allowFileTypes.Contains(fileExt))
                throw new UserFriendlyException($"Wrong file type {file.ContentType}. Allow file types: {string.Join(", ", allowFileTypes)}");
        }

        public async Task<string> UploadAvatarAsync(IFormFile file)
        {
            CheckValidFile(file, ConstantUploadFile.AllowImageFileTypes);

            string fileLocation = UploadFile.CreateFolderIfNotExists(WWWRootFolder, ConstantUploadFile.AvatarFolder);

            var fileName = $"{CommonUtil.NowToYYYYMMddHHmmss()}_{Guid.NewGuid()}.{FileUtils.GetFileExtension(file)}";
            var filePath = $"{ConstantUploadFile.AvatarFolder?.TrimEnd('/')}/{fileName}";

            await UploadFile.UploadFileAsync(fileLocation, file, fileName);

            return filePath;
        }

        public Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, string filePath)
        {
            throw new NotImplementedException();
        }

    }
}
