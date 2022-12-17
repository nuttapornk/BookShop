using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Newtonsoft.Json;
using NuGet.Configuration;

namespace BookShop.WebUi.Helpers
{
    public class TokenExchange
    {
        static readonly HttpClient _httpClient = new();
        public TokenExchange()
        {

        }

        public async Task<string> GetRefreshTokenAsync(string refreshToken,IConfiguration configuration)
        {
            string url = configuration["Keycloak:TokenExchange"];
            string grantType = "refresh_token";
            string clientId = configuration["Keycloak:ClientId"];
            string clientSecret = configuration["Keycloak:ClientSecret"];
            string token = refreshToken;

            Dictionary<string, string> form = new()
            {
                {"grant_type",grantType},
                {"client_id",clientId},
                {"client_secret",clientSecret },
                {"refresh_token",token }
            };
            HttpResponseMessage response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(form));
            var jsonContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(jsonContent).AccessToken;
        }

        public async Task<string> GetTokenExchange(string accessToken, IConfiguration configuration)
        {
            string url = configuration["Keycloak:TokenExchange"];
            string grantType = "urn:ietf:params:oauth:grant-type:token-exchange";
            string clientId = configuration["Keycloak:ClientId"];
            string clientSecret = configuration["Keycloak:ClientSecret"];
            string audience = configuration["Keycloak:Audience"];
            string token = accessToken;

            Dictionary<string, string> form = new()
            {
                    {"grant_type", grantType},
                    {"client_id", clientId},
                    {"client_secret", clientSecret},
                    {"audience", audience},
                    {"subject_token", token }
            };

            HttpResponseMessage tokenResponse = await _httpClient.PostAsync(url, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(jsonContent).AccessToken;
        }

        internal class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expirse_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }

    }
}
