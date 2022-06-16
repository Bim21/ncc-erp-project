
using Abp.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement.Constants;
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
        private readonly string _channelIdDevMode;
        private bool _enableSendToKomu = false;

        public KomuService(HttpClient httpClient, ILogger<KomuService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.httpClient = httpClient;
             _channelIdDevMode = configuration.GetValue<string>("KomuService:DevModeChannelId");
            var _isNotifyToKomu = configuration.GetValue<string>("KomuService:EnableKomuNotification");
            _enableSendToKomu = _isNotifyToKomu == "true";
            var baseAddress = configuration.GetValue<string>("KomuService:BaseAddress");
            var secretCode = configuration.GetValue<string>("KomuService:SecurityCode");

            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", secretCode);
        }
        public async Task<long?> GetKomuUserId(KomuUserDto input)
        {
            var komuUser = await PostAsync<KomuUserDto>(ChannelTypeConstant.KOMU_USER, new { username = input.Username });
            if (komuUser != null)
                return komuUser.KomuUserId;

            return default;
        }
        public void NotifyToChannel(KomuMessage input, string channelType)
        {
            if (!_enableSendToKomu)
            {
                logger.LogInformation("_enableSendToKomu=false => stop");
            }

            if (!string.IsNullOrEmpty(_channelIdDevMode))
            {                
                 Post(ChannelTypeConstant.KOMU_CHANNELID, new { message = input.Message, channelid = _channelIdDevMode });
            }
            else
            {
                 Post(channelType, input);
            }
        }


        public void Post(string url, object input)
        {
            var fullUrl = $"{this.httpClient.BaseAddress}/{url}";
            string strInput = JsonConvert.SerializeObject(input);
            try
            {
                logger.LogInformation($"Post: {fullUrl} input: {strInput}");
                var contentString = new StringContent(strInput, Encoding.UTF8, "application/json");
                httpClient.PostAsync(url, contentString);
            }
            catch (Exception e)
            {
                logger.LogError($"Post: {fullUrl} input: {strInput} Error: {e.Message}");
            }

        }
        public async Task<T> PostAsync<T>(string url, object input)
        {
            string strInput = JsonConvert.SerializeObject(input);
            var fullUrl = $"{this.httpClient.BaseAddress}/{url}";
            try
            {
                logger.LogInformation($"Post: {fullUrl} input: {strInput}");

                var contentString = new StringContent(strInput, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(url, contentString);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    logger.LogInformation($"Post: {fullUrl} input: {strInput} response: {responseContent}");

                    JObject responseJObj = JObject.Parse(responseContent);

                    var result = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(responseJObj["result"]));
                    if (result == null)
                    {
                        result = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(responseJObj));
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                logger.LogError($"Post: {fullUrl} input: {strInput} Error: {e.Message}");
            }

            return default;


        }


    }
}