using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement.Services.Timesheet.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Timesheet
{
    public class TimesheetService : BaseWebService
    {
        private const string serviceName = "TimesheetService";
        public TimesheetService(
            HttpClient httpClient, 
            IConfiguration configuration,
            ILogger<TimesheetService> logger
        ) : base(httpClient,configuration,logger,serviceName)
        {
        }

        public async Task<string> CreateCustomer(string name, string code, string address)
        {
            var item = new
            {
                Name = name,
                Code = code,
                Address = address
            };
            return await PostAsync<string>($"/api/services/app/ProjectManagement/CreateCustomer", item);
        }
        public async Task<string> CreateProject(string name, string code,
                            DateTime startTime, DateTime? endTime, string customerCode, ProjectType projectType, string emailPM)
        {
            var item = new
            {
                Name = name,
                Code = code,
                TimeStart = startTime,
                TimeEnd = endTime,
                CustomerCode = customerCode,
                ProjectType = projectType,
                EmailPM = emailPM
            };
            return await PostAsync<string>($"/api/services/app/ProjectManagement/CreateProject", item);
        }

       
        public async Task<TimesheetTaxDto> GetTimesheetDetailForTax(TimesheetDetailForTaxDto input)
        {
            return await PostAsync<TimesheetTaxDto>($"/api/services/app/Public/GetTimesheetDetailForTax", input);
        }

        public async Task<string> ChangePmOfProject(string code, string EmailPM)
        {
            var item = new
            {
                Code = code,
                EmailPM = EmailPM
            };
            return await PostAsync<string>($"/api/services/app/ProjectManagement/ChangePmOfProject", item);
        }

        public async Task<string> CloseProject(string code)
        {            
            return await PostAsync<string>($"/api/services/app/ProjectManagement/CloseProject?code=" + code, null);
        }


        public async Task<TotalWorkingTimeOfWeekDto> GetWorkingHourFromTimesheet(string projectCode, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<TotalWorkingTimeOfWeekDto>($"api/services/app/ProjectManagement/GetTotalWorkingTime?projectCode={projectCode}&startDate={startDate.ToString("yyyy/MM/dd")}&endDate={endDate.ToString("yyyy/MM/dd")}");
        }

        public async Task<List<TotalWorkingTimeOfWeekDto>> GetTimesheetByListProjectCode(List<string> listProjectCode, DateTime startDate, DateTime endDate)
        {
            return await PostAsync<List<TotalWorkingTimeOfWeekDto>>($"api/services/app/ProjectManagement/GetTimesheetByListProjectCode" +
                $"?startDate={startDate.ToString("yyyy/MM/dd")}" +
                $"&endDate={endDate.ToString("yyyy/MM/dd")}",
                listProjectCode);
        }


      
        public void UserJoinProject(string projectCode, string emailAddress, bool isPool, ProjectUserRole role)
        {
            var item = new
            {
                ProjectCode = projectCode,
                EmailAddress = emailAddress,
                IsPool = isPool,
                Role = role,
            };
            Post($"/api/services/app/ProjectManagement/UserJoinProject", item);
        }

      

    }
}
