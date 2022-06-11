using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Utils
{
    public class FileUtils
    {
        public static string FullFilePath(string filePath)
        {
            if (Constants.ConstantUploadFile.Provider == Constants.ConstantUploadFile.AmazoneS3)
            {
                return Constants.ConstantAmazonS3.CloudFront.TrimEnd('/') + "/" + filePath;
            }
            else
            {
                return Constants.ConstantInternalUploadFile.RootUrl.TrimEnd('/') + "/" + filePath;
            }
        }
    }
}
