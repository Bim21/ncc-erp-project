using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public class InternalUploadFileService : IFileService
    {
        private readonly string wwwRootFolder = "wwwroot";
        private readonly UserManager _userManager;



        public InternalUploadFileService(HttpClient httpClient, UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, long userId)
        {
            CheckValidFile(file, Constants.ConstantUploadFile.AllowImageFileTypes);
            var userInfo = await ImageUserInfo(userId);
            string fileLocation = UploadFile.CreateFolderIfNotExists(wwwRootFolder, Constants.ConstantInternalUploadFile.AvatarFolder);

            string fileName = await UploadFile.UploadImageAsync(fileLocation, file, userInfo);

            return Constants.ConstantInternalUploadFile.AvatarFolder + userInfo + "_" + file.FileName;
        }

        private void CheckValidFile(IFormFile file, string[] allowFileTypes)
        {
            var fileExt = Path.GetExtension(file.FileName).Substring(1).ToLower();
            if (!allowFileTypes.Contains(fileExt))
                throw new UserFriendlyException($"Wrong file type {file.ContentType}. Allow file types: {string.Join(", ", allowFileTypes)}");
        }

        public async Task<string> UploadImageFileAsync(IFormFile file, long userId)
        {
            return await UploadFileAsync(file, Constants.ConstantUploadFile.AllowImageFileTypes, userId);
        }

        public async Task<string> ImageUserInfo(long userId)
        {
            User user = await _userManager.GetUserByIdAsync(userId);
            string path = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                            + "_" + user.UserName;
            return path;

        }

    }
}
