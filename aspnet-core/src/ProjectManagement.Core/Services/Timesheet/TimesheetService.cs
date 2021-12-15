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

namespace ProjectManagement.Services.Timesheet
{
    public class TimesheetService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<FinanceService> logger;
        private readonly ISettingManager settingManager;

        public TimesheetService(HttpClient httpClient, ILogger<FinanceService> logger, ISettingManager settingManager)
        {
            this.settingManager = settingManager;
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<TotalWorkingTimeOfWeekDto> GetWorkingHourFromTimesheet(string projectCode, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<TotalWorkingTimeOfWeekDto>($"api/services/app/ProjectManagement/GetTotalWorkingTime?projectCode={projectCode}&startDate={startDate.ToString("yyyy/MM/dd")}&endDate={endDate.ToString("yyyy/MM/dd")}");
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
                    logger.LogInformation($"Get: {Url} responseCode: {response.StatusCode}");
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Khong the ket noi Timesheet");
                }
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
                try { response = await httpClient.PostAsync(Url, contentString); }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Khong the ket noi Timesheet");
                }
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
        }
    }
}
