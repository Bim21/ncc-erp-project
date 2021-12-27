
using Abp.Configuration;
using Abp.UI;
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
        private HttpClient httpClient;
        private string _baseUrl;
        private string _secretCode;

        public KomuService(HttpClient httpClient, ILogger<KomuService> logger, ISettingManager settingManager)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            _baseUrl = settingManager.GetSettingValueForApplication(AppSettingNames.KomuUrl);
            _secretCode = settingManager.GetSettingValueForApplication(AppSettingNames.KomuSecretCode);
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
            url = _baseUrl + url;
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", _secretCode);
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                httpResponse = await httpClient.PostAsync(url, contentString);
            }
            catch (Exception e)
            {
                logger.LogError($"Komu: {url} Response() => {httpResponse.StatusCode}/Error() => {e.Message}");
                //throw new UserFriendlyException("Connection to KOMU failed!");
            }
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                logger.LogInformation($"Komu: {url} Response() => {httpResponse.StatusCode}: {responseContent}");
            }
            return httpResponse;
        }
    }
}