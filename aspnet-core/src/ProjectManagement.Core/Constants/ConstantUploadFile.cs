using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Constants
{
    public class ConstantUploadFile
    {
        public static string Provider { get; set; }
        public static string[] AllowImageFileTypes { get; set; }


        public static readonly string AmazoneS3 = "AWS";
        public static readonly string InternalUploadFile = "InternalUploadFile";
    }
}
