
using Abp.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Authorization.Users;
using ProjectManagement.Configuration;
using ProjectManagement.Services.Komu.KomuDto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectManagement.Services.Komu
{
    public class KomuService
    {
        private HttpClient HttpClient = new HttpClient();
        private ILogger<KomuService> logger;
        private ISettingManager settingManager;

        public KomuService(HttpClient httpClient, ILogger<KomuService> logger, ISettingManager settingManager)
        {
            this.logger = logger;
            this.settingManager = settingManager;
            this.HttpClient.BaseAddress = new Uri(settingManager.GetSettingValueForApplication(AppSettingNames.KomuUrl));
            this.HttpClient.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<HttpResponseMessage> Login(LoginDto input)
        {
            return await PostAsync("login", input, null);
        }
        public async Task<HttpResponseMessage> PostMessage(PostMessage input, HeaderIfRequerid header)
        {
            return await PostAsync("chat.postMessage", input, header);
        }
        public async Task<HttpResponseMessage> Logout(HeaderIfRequerid header)
        {
            return await PostAsync("logout", new object { }, header);
        }

        private async Task<HttpResponseMessage> PostAsync(string url, object input, HeaderIfRequerid header)
        {
            logger.LogInformation($"Post: {url}");
            var contentString = new StringContent(JsonConvert.SerializeObject(input));
            if (header == null)
            {
                contentString.Headers.ContentType.MediaType = "application/json";
            }
            else
            {
                contentString.Headers.ContentType.MediaType = "application/json";
                contentString.Headers.ContentType.CharSet = "";
                contentString.Headers.Add("X-Auth-Token", header.AuthToken);
                contentString.Headers.Add("X-User-Id", header.userId);
            }
            HttpClient.DefaultRequestHeaders.Host = "komu.vn";
            var uri = await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.KomuUrl) + url;
            var response = await HttpClient.PostAsync(uri, contentString);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                logger.LogInformation(responseContent);
                return response;
            }
            return response;
        }
        public async Task ProcessKomu(List<attachment> ListAttach, string message, string alias, List<string> komuUserNames)
        {
            var login = new LoginDto
            {
                password = await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.PasswordBot),
                user = await settingManager.GetSettingValueForApplicationAsync(AppSettingNames.UserBot)
            };
            var response = await this.Login(login);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var DecryptContent = JsonConvert.DeserializeObject<LoginJsonPrase>(responseContent);
                foreach (var i in komuUserNames)
                {
                    var postMessage = new PostMessage
                    {
                        channel = "@" + i,
                        text = message.ToString(),
                        alias = alias,
                        attachments = ListAttach
                    };
                    await this.PostMessage(postMessage, DecryptContent.data);
                }
                await this.Logout(DecryptContent.data);
            }
        }
    }
}
