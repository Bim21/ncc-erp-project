using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.TimesheetProjects.Dto
{
    public class TotalAmountDto
    {
        public double AmountVND { get; set; }
        public double AmountUSD { get; set; }
        public double RoundAmountVND => Math.Round(AmountVND);
        public double RoundAmountUSD => Math.Round(AmountUSD);
    }
}
