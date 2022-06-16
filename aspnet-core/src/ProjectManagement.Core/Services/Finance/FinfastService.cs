using Abp.Configuration;
using Abp.UI;
using Microsoft.Extensions.Configuration;
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
    public class FinfastService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<FinfastService> logger;

        public FinfastService(HttpClient httpClient, ILogger<FinfastService> logger, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            var baseAddress = configuration.GetValue<string>("FinfastService:BaseAddress");
            var securityCode = configuration.GetValue<string>("FinfastService:SecurityCode");
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.BaseAddress = new Uri(baseAddress);
            this.httpClient.DefaultRequestHeaders.Add("X-Secret-Key", securityCode);

        }
        public async Task<CreateInvoiceDto> CreateInvoiceToFinance(List<CreateInvoiceDto> input)
        {
            return await PostAsync<CreateInvoiceDto>($"api/services/app/ProjectManagement/CreateInvoice", input);
        }

        private async Task<T> GetAsync<T>(string url)
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
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Get: {fullUrl} error: { ex.Message}");
            }

            return default;

        }
        private async Task<T> PostAsync<T>(string url, object input)
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
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Post: {fullUrl} error: { ex.Message}");
            }
            return default;

        }
    }
}
