using Abp.Configuration;
using Abp.UI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement.Configuration;
using ProjectManagement.Services.HRM.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services.HRM
{
    public class HrmService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<HrmService> logger;
        private readonly ISettingManager settingManager;

        public HrmService(HttpClient httpClient, ILogger<HrmService> logger, ISettingManager settingManager)
        {
            this.settingManager = settingManager;
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<List<AutoUpdateUserDto>> GetUserFromHRM()
        {
            return await GetAsync<List<AutoUpdateUserDto>>($"/api/services/app/ProjectManagement/GetAllUser");
        }
        public async Task<AutoUpdateUserDto> GetUserFromHRMByEmail(string email)
        {
            return await GetAsync<AutoUpdateUserDto>($"/api/services/app/ProjectManagement/GetUserByEmail?email={email}");
        }

        private async Task<T> GetAsync<T>(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.BaseAddress = new Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.HRMUri));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.HRMSecretCode));
                HttpResponseMessage response = new HttpResponseMessage();
                try 
                { 
                    response = await httpClient.GetAsync(Url);
                    logger.LogInformation($"Get: {Url} responseCode: {response.StatusCode}");
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Khong the ket noi HRM");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"Get: {Url} response: { responseContent}");
                    //JObject res = JObject.Parse(responseContent);
                    //var rs = JsonConvert.DeserializeObject<AuthenticateResultModelDto>(JsonConvert.SerializeObject(res["result"]));
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
                httpClient.BaseAddress = new Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.HRMUri));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.HRMSecretCode));
                var contentString = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                HttpResponseMessage response = new HttpResponseMessage();
                try 
                { 
                    response = await httpClient.PostAsync(Url, contentString);
                    logger.LogInformation($"Post: {Url} responseCode: {response.StatusCode}");
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Khong the ket noi HRM");
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
