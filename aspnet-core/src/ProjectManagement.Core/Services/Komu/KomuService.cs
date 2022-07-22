
using Abp.Configuration;
using Abp.Runtime.Session;
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
    public class KomuService : BaseWebService
    {
        private readonly string _channelIdDevMode;
        private bool _enableSendToKomu = false;
        private const string serviceName = "KomuService";

        public KomuService(
            HttpClient httpClient, 
            ILogger<KomuService> logger, 
            IConfiguration configuration,
            IAbpSession abpSession
        ) : base(httpClient,configuration,logger, abpSession, serviceName)
        {
             _channelIdDevMode = configuration.GetValue<string>($"{serviceName}:DevModeChannelId");
            var _isNotifyToKomu = configuration.GetValue<string>($"{serviceName}:EnableKomuNotification");
            _enableSendToKomu = _isNotifyToKomu == "true";
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
    }
}