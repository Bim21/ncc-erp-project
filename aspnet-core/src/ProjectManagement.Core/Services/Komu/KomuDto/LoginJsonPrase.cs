
using System.Text.Json.Serialization;

namespace ProjectManagement.Services.Komu.KomuDto
{
    public class LoginJsonPrase
    {
        [JsonPropertyName("status")]
        public string status { get; set; }
        [JsonPropertyName("data")]
        public HeaderIfRequerid data { get; set; }
    }
}
