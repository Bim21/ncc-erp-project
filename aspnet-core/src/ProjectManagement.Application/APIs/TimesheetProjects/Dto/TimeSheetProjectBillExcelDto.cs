using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class TimeSheetProjectBillExcelDto
    {
        public string FullName { get; set; }
        public float WorkingTime { get; set; }
        public double BillRate { get; set; }
    }
    public class FileBase64Dto
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Base64 { get; set; }
    }
}
