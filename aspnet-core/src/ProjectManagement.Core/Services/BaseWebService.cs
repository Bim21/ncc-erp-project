using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services
{
    public abstract class BaseWebService
    {
        private readonly HttpClient httpClient;
        protected readonly ILogger logger;
        private string serviceName = string.Empty;
        public BaseWebService(
            HttpClient httpClient, 
            IConfiguration configuration, 
            ILogger logger, 
            string serviceName
        )
        {
            this.httpClient = httpClient;
            this.logger = logger;
            this.serviceName = serviceName;
            GetConfigService(configuration);
        }
        protected virtual async Task<T> GetAsync<T>(string url) 
        {
            var fullUrl = $"{ httpClient.BaseAddress }/{ url}";
            try
            {
                logger.LogInformation($"Get: {fullUrl}");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"Get: {fullUrl} response: { responseContent}");
                    JObject responseJObj = JObject.Parse(responseContent);
                    if (responseJObj.ContainsKey("result"))
                    {
                        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(responseJObj["result"]));
                    }
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Get: {fullUrl} error: { ex.Message}");
            }

            return default;

        }
        protected virtual async Task<T> PostAsync<T>(string url, object input) 
        {
            var fullUrl = $"{ httpClient.BaseAddress }/{ url}";
            var strInput = JsonConvert.SerializeObject(input);
            var contentString = new StringContent(strInput, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(url, contentString);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"Post: {fullUrl} input: {strInput} response: { responseContent}");
                    JObject responseJObj = JObject.Parse(responseContent);
                    if (responseJObj.ContainsKey("result")){
                       return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(responseJObj["result"]));
                    }
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Post: {fullUrl} error: { ex.Message}");
            }
            return default;
        }
        public void Post(string url, object input)
        {
            var fullUrl = $"{httpClient.BaseAddress}/{url}";
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
        private void GetConfigService(IConfiguration configuration)
        {
            var baseAddress = configuration.GetValue<string>($"{serviceName}:BaseAddress");
            var securityCode = configuration.GetValue<string>($"{serviceName}:SecurityCode");
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.BaseAddress = new Uri(baseAddress);
            this.httpClient.DefaultRequestHeaders.Add("X-Secret-Key", securityCode);
        }
    }
}
