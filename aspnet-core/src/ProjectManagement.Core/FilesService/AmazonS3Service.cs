using Abp.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.FilesService
{
    public class AmazonS3Service : IFileService
    {
        private readonly ILogger<AmazonS3Service> logger;
        private readonly IAmazonS3 s3Client;
        private readonly UserManager _userManager;

        public AmazonS3Service(HttpClient httpClient, ILogger<AmazonS3Service> logger, IAmazonS3 _s3Client, UserManager userManager)
        {
            this.logger = logger;
            this.s3Client = _s3Client;
            _userManager = userManager;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string[] allowFileTypes, long userId)
        {
            var userInfo = await ImageUserInfo(userId);
            var strAlowFileType = string.Join(", ", allowFileTypes);
            logger.LogInformation($"UploadFile() fileName: {file.FileName}, contentType: {file.ContentType}, allowFileTypes: {strAlowFileType}");

            CheckValidFile(file, allowFileTypes);

            var key = string.IsNullOrEmpty(ConstantAmazonS3.Prefix) ? file.FileName : $"{ConstantAmazonS3.Prefix?.TrimEnd('/')}/{userInfo}_{file.FileName}";

            logger.LogInformation($"UploadImageFile() Key: {key}");
            var request = new PutObjectRequest()
            {
                BucketName = ConstantAmazonS3.BucketName,
                Key = key,
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await s3Client.PutObjectAsync(request);
            /* PutObjectResponse response = await s3Client.PutObjectAsync(request);*/
            /*  logger.LogInformation(response.ToString());*/
            return key;
        }

        private void CheckValidFile(IFormFile file, string[] allowFileTypes)
        {
            var fileExt = Path.GetExtension(file.FileName).Substring(1).ToLower(); ;
            if (!allowFileTypes.Contains(fileExt))
                throw new UserFriendlyException($"Wrong file type {file.ContentType}. Allow file types: {string.Join(", ", allowFileTypes)}");
        }

        public async Task<string> UploadImageFileAsync(IFormFile file, long userId)
        {
            return await UploadFileAsync(file, ConstantUploadFile.AllowImageFileTypes, userId);
        }

        public async Task<string> ImageUserInfo(long userId)
        {
            User user = await _userManager.GetUserByIdAsync(userId);
            string path = user.UserName
                            + "_" + DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return path;

        }

    }
}
