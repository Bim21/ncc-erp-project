
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
        private HttpClient httpClient;

        public KomuService(HttpClient httpClient, ILogger<KomuService> logger, ISettingManager settingManager)
        {
            this.logger = logger;
            this.settingManager = settingManager;
            this.httpClient = httpClient;
        }
        public async Task<long?> GetKomuUserId(KomuUserDto komuUserDto, string url)
        {
            var contentString = new StringContent(JsonConvert.SerializeObject(komuUserDto), Encoding.UTF8, "application/json");
            var httpResponse = await PostAsync(url, contentString);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KomuUserDto>(responseContent);
                return result != null ? result.KomuUserId : null;
            }
            return null;
        }
        public async Task<HttpResponseMessage> NotifyToChannel(KomuMessage input, string channelType)
        {
            var contentString = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            return await PostAsync(channelType, contentString);
        }
        private async Task<HttpResponseMessage> PostAsync(string url, StringContent contentString)
        {
            logger.LogInformation($"Komu: {url}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new System.Uri(await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUrl));
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", $"{await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuSecretCode)}");
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