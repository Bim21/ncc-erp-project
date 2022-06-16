using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement.Services.HRM.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services.HRM
{
    public class HRMService
    {
        private HttpClient httpClient;
        private readonly ILogger<HRMService> logger;

        public HRMService(HttpClient httpClient, IConfiguration configuration, ILogger<HRMService> logger)
        {
            this.logger = logger;
            this.httpClient = httpClient;

            var baseAddress = configuration.GetValue<string>("HRMService:BaseAddress");
            var securityCode = configuration.GetValue<string>("HRMService:SecurityCode");

            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", securityCode);
        }

        public async Task<List<AutoUpdateUserDto>> GetUserFromHRM()
        {
            return await GetAsync<List<AutoUpdateUserDto>>($"/api/services/app/ProjectManagement/GetAllUser");
        }
        public async Task<AutoUpdateUserDto> GetUserFromHRMByEmail(string email)
        {
            return await GetAsync<AutoUpdateUserDto>($"/api/services/app/ProjectManagement/GetUserByEmail?email={email}");
        }


        public async Task<T> GetAsync<T>(string url)
        {
            var fullUrl = $"{ httpClient.BaseAddress }/{ url}";
            try
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"Get: {fullUrl} => Response: {responseContent}");
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
                logger.LogError($"Get: {fullUrl} => Exception: {e}");                
            }
            return default;

        }

        public void Post(string url, object input)
        {
            var fullUrl = $"{ httpClient.BaseAddress }/{ url}";
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
            var fullUrl = $"{ httpClient.BaseAddress }/{ url}";
            string strInput = JsonConvert.SerializeObject(input);
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
