using Abp.Configuration;
using Abp.UI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Configuration;
using ProjectManagement.Services.Finance.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services.Finance
{
    public class FinanceService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<FinanceService> logger;
        private readonly ISettingManager settingManager;

        public FinanceService(HttpClient httpClient, ILogger<FinanceService> logger, ISettingManager settingManager)
        {
            this.settingManager = settingManager;
            this.httpClient = httpClient;
            this.logger = logger;
        }
        public async Task<CreateInvoiceDto> CreateInvoiceToFinance(List<CreateInvoiceDto> input)
        {
            return await PostAsync<CreateInvoiceDto>($"api/services/app/ProjectManagement/CreateInvoice", input);
        }

        private async Task<T> GetAsync<T>(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.BaseAddress = new Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinaceUri));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinanceSecretKey));
                HttpResponseMessage response = new HttpResponseMessage();
                try { response = await httpClient.GetAsync(Url); }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Khong the ket noi Finance");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"Get: {Url} response: { responseContent}");
                    return JsonConvert.DeserializeObject<T>(responseContent);
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
                httpClient.BaseAddress = new System.Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinaceUri));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.FinanceSecretKey));
                var contentString = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                HttpResponseMessage response = new HttpResponseMessage();
                try { response = await httpClient.PostAsync(Url, contentString); }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Khong the ket noi Finance");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"Post: {Url} response: { responseContent}");
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                else
                {
                    return default;
                }
            }
        }
    }
}
