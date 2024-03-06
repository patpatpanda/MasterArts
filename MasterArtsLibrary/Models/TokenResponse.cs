using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MasterArtsLibrary.Models
{
    

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Access_token { get; set; }

        [JsonPropertyName("token_type")]
        public string Token_type { get; set; }

        [JsonPropertyName("expires_in")]
        public int Expires_in { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("refresh_token")]
        public string Refresh_token { get; set; }
    }


}
