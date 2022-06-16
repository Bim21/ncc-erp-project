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
            return file.FileName.Contains(".") ? file.FileName.Split(".")[1] : file.FileName;
        }
    }
}
