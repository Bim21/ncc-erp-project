using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Utils
{
    public class FileUtils
    {
        public static string FullFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
            if (Constants.ConstantUploadFile.Provider == Constants.ConstantUploadFile.AMAZONE_S3)
            {
                return Constants.ConstantAmazonS3.CloudFront.TrimEnd('/') + "/" + filePath;
            }
            else
            {
                return Constants.ConstantInternalUploadFile.RootUrl.TrimEnd('/') + "/" + filePath;
            }
        }

        public static string GetFileExtension(IFormFile file)
        {
            if (file == default || string.IsNullOrEmpty(file.FileName))
            {
                return "";
            }
            string[] arr = file.FileName.Split(".");            
            return arr.Length > 1 ? arr[arr.Length - 1] : file.FileName;
        }

        public static string GetFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
            if (filePath.Contains("/"))
            {
                return filePath.Substring(filePath.LastIndexOf("/") + 1);
            }
            return filePath;

        }

    }
}
