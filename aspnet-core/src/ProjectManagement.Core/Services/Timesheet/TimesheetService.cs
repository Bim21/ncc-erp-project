using Abp.Configuration;
using Abp.UI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement.Configuration;
using ProjectManagement.Services.Finance;
using ProjectManagement.Services.Timesheet.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Timesheet
{
    public class TimesheetService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<TimesheetService> logger;
        private readonly ISettingManager settingManager;

        public TimesheetService(HttpClient httpClient, ILogger<TimesheetService> logger, ISettingManager settingManager)
        {
            this.settingManager = settingManager;
            this.httpClient = httpClient;
            this.logger = logger;
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

        private async Task<T> GetAsync<T>(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.BaseAddress = new Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetSecretCode));
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await httpClient.GetAsync(Url);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        logger.LogInformation($"Get: {Url} response: { responseContent}");
                        JObject res = JObject.Parse(responseContent);
                        var rs = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(res["result"]));
                        return rs;
                    }
                    else
                    {
                        return default;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Exception in GetAsync() url = {Url}\nError = {ex.Message}");
                    return default;
                }
                
            }
        }
        private async Task<T> PostAsync<T>(string Url, object input)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.BaseAddress = new System.Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetUri));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.TimesheetSecretCode));
                var contentString = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                HttpResponseMessage response = new HttpResponseMessage();
                try { 
                    response = await httpClient.PostAsync(Url, contentString);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        logger.LogInformation($"Post: {Url} response: { responseContent}");
                        JObject res = JObject.Parse(responseContent);
                        var rs = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(res["result"]));
                        return rs;
                    }
                    else
                    {
                        return default;
                    }
                }
                catch (Exception ex)
                {
                   logger.LogError($"Exception in PostAsync() url = {Url}, input={JObject.FromObject(input).ToString()}\nError = {ex.Message}");
                    return default;
                }
                
            }
        }
    }
}
