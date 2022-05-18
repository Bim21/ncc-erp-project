
using Abp.Configuration;
using Abp.UI;
using Microsoft.Extensions.Configuration;
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
        private string channelUrl = string.Empty;
        private string channelId = string.Empty;
        private bool _enableSendToKomu = false;

        public KomuService(HttpClient httpClient, ILogger<KomuService> logger, ISettingManager settingManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            channelUrl = configuration.GetValue<string>("Channel:ChannelUrl");
            channelId = configuration.GetValue<string>("Channel:ChannelId");
            _baseUrl = settingManager.GetSettingValueForApplication(AppSettingNames.KomuUrl);
            _secretCode = settingManager.GetSettingValueForApplication(AppSettingNames.KomuSecretCode);
            var noticeToKomu = settingManager.GetSettingValueForApplication(AppSettingNames.NoticeToKomu);
            _enableSendToKomu = noticeToKomu == "true";
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
        public void NotifyToChannel(KomuMessage input, string channelType)
        {
            if (!_enableSendToKomu)
            {
                logger.LogInformation("_enableSendToKomu=false => stop");
            }

            if (!string.IsNullOrEmpty(channelUrl) && !string.IsNullOrEmpty(channelId))
            {
                var contentString = new StringContent(JsonConvert.SerializeObject(new { message = input.Message, channelid = channelId }), Encoding.UTF8, "application/json");
                 Post(channelUrl, contentString);
            }
            else
            {
                var contentString = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                 Post(channelType, contentString);

            }
        }
        private async Task<HttpResponseMessage> PostAsync(string url, StringContent contentString)
        {
            url = _baseUrl + url;
            logger.LogInformation($"PostAsync() url={url}");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", _secretCode);
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                httpResponse = await httpClient.PostAsync(url, contentString);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    logger.LogInformation($"PostAsync() url={url} Response:StatusCode= {httpResponse.StatusCode}, Content={responseContent}");
                }
                return httpResponse;
            }
            catch (Exception e)
            {
                logger.LogInformation($"PostAsync() url={url} Response:StatusCode= {httpResponse.StatusCode}, Error={e.Message}");
                //throw new UserFriendlyException("Connection to KOMU failed!");
                return null;
            }

        }

        private void Post(string url, StringContent contentString)
        {
            url = _baseUrl + url;
            logger.LogInformation($"Post() url={url}");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", _secretCode);
            try
            {
                httpClient.PostAsync(url, contentString);
            }
            catch (Exception e)
            {
                logger.LogInformation($"Post() url={url}, Error={e.Message}");
            }

        }
    }
}