
using Abp.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Configuration;
using ProjectManagement.Services.Komu.KomuDto;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services.Komu
{
    public class KomuService
    {
        private ILogger<KomuService> logger;
        private ISettingManager settingManager;

        public KomuService(ILogger<KomuService> logger, ISettingManager settingManager)
        {
            this.logger = logger;
            this.settingManager = settingManager;
        }
        public async Task<HttpResponseMessage> NotifyToChannel(KomuMessage input, string channelType)
        {
            return await PostAsync(channelType, input);
        }
        private async Task<HttpResponseMessage> PostAsync(string url, KomuMessage input)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                logger.LogInformation($"Komu: {url}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.BaseAddress = new System.Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUrl));
                httpClient.DefaultRequestHeaders.Add("X-Secret-Key", $"{await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuSecretCode)}");
                var contentString = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                try
                {
                    httpResponse = await httpClient.PostAsync(url, contentString);
                }
                catch (Exception e)
                {
                    logger.LogError($"Komu:{httpClient.BaseAddress}{url} Response() => {httpResponse.StatusCode}/Error() => {e.Message}");
                    //throw new UserFriendlyException("Connection to KOMU failed!");
                }
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    logger.LogInformation($"Komu:{httpClient.BaseAddress}{url} Response() => {httpResponse.StatusCode}: {responseContent}");
                    return httpResponse;
                }
                return httpResponse;
            }
        }
    }
}