﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.Timesheet.Dto
{
    public class InputTimesheetTaxDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<string> ProjectCodes { get; set; }
    }
}
