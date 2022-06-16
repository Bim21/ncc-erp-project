using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement.Services.Timesheet.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Services.Timesheet
{
    public class TimesheetService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<TimesheetService> logger;

        public TimesheetService(HttpClient httpClient, ILogger<TimesheetService> logger, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.logger = logger;

            var baseAddress = configuration.GetValue<string>("TimesheetService:BaseAddress");
            var securityCode = configuration.GetValue<string>("TimesheetService:SecurityCode");

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Add("X-Secret-Key", securityCode);
        }

        public async Task<string> CreateCustomer(string name, string code, string address)
        {
            var item = new
            {
                Name = name,
                Code = code,
                Address = address
            };
            return await PostAsync<string>($"/api/services/app/ProjectManagement/CreateCustomer", item);
        }
        public async Task<string> CreateProject(string name, string code,
                            DateTime startTime, DateTime? endTime, string customerCode, ProjectType projectType, string emailPM)
        {
            var item = new
            {
                Name = name,
                Code = code,
                TimeStart = startTime,
                TimeEnd = endTime,
                CustomerCode = customerCode,
                ProjectType = projectType,
                EmailPM = emailPM
            };
            return await PostAsync<string>($"/api/services/app/ProjectManagement/CreateProject", item);
        }

       
        public async Task<TimesheetTaxDto> GetTimesheetDetailForTax(TimesheetDetailForTaxDto input)
        {
            return await PostAsync<TimesheetTaxDto>($"/api/services/app/Public/GetTimesheetDetailForTax", input);
        }

        public async Task<string> ChangePmOfProject(string code, string EmailPM)
        {
            var item = new
            {
                Code = code,
                EmailPM = EmailPM
            };
            return await PostAsync<string>($"/api/services/app/ProjectManagement/ChangePmOfProject", item);
        }

        public async Task<string> CloseProject(string code)
        {            
            return await PostAsync<string>($"/api/services/app/ProjectManagement/CloseProject?code=" + code, null);
        }


        public async Task<TotalWorkingTimeOfWeekDto> GetWorkingHourFromTimesheet(string projectCode, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<TotalWorkingTimeOfWeekDto>($"api/services/app/ProjectManagement/GetTotalWorkingTime?projectCode={projectCode}&startDate={startDate.ToString("yyyy/MM/dd")}&endDate={endDate.ToString("yyyy/MM/dd")}");
        }

        public async Task<List<TotalWorkingTimeOfWeekDto>> GetTimesheetByListProjectCode(List<string> listProjectCode, DateTime startDate, DateTime endDate)
        {
            return await PostAsync<List<TotalWorkingTimeOfWeekDto>>($"api/services/app/ProjectManagement/GetTimesheetByListProjectCode" +
                $"?startDate={startDate.ToString("yyyy/MM/dd")}" +
                $"&endDate={endDate.ToString("yyyy/MM/dd")}",
                listProjectCode);
        }


        public async Task<T> GetAsync<T>(string url)
        {
            var fullUrl = $"{httpClient.BaseAddress}/{url}";
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
        public async Task<T> PostAsync<T>(string url, object input)
        {
            var fullUrl = $"{httpClient.BaseAddress}/{url}";
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
