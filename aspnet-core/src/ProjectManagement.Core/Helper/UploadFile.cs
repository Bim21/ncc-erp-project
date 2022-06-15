using Abp.UI;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Helper
{
    public class UploadFile
    {
        public static async Task<string> UploadFileAsync(string fileLocation, IFormFile file, string fileName)
        {
            var fullFilePath = Path.Combine(fileLocation, fileName);
            using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        public static string CreateFolderIfNotExists(string path1, string path2)
        {
            var fileLocation = Path.Combine(path1, path2);
            if (!Directory.Exists(fileLocation))
                Directory.CreateDirectory(fileLocation);

            return fileLocation;
        }
    }
}
