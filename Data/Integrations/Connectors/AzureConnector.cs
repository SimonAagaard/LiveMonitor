using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Integrations
{
    public class AzureConnector
    {
        private readonly string _clientId = "2de6a64e-eb0c-4275-9952-4ce1d3f0d131";
        private readonly string _clientSecret = "A1l3?[ReU3?L8eEhaYpcUPJG]jEX0_X5";
        private readonly string _tenantId = "92404485-d794-4fc2-8d0d-587d30cba2ad";
        private readonly string _resourceId = "api://2de6a64e-eb0c-4275-9952-4ce1d3f0d131";

        public AzureConnector()
        {
            //string clientId, string clientSecret, string tenantId
            //_clientId = clientId;
            //_clientSecret = clientSecret;
            //_tenantId = tenantId;
        }

        public async Task<AuthServerResponse> GetAuthTokenAsync()
        {
            try
            {
                using HttpClient client = new HttpClient();
                // Clear default headers and set the security protocol
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                // Set baseadress for authentication
                Uri authUrl = new Uri("https://login.microsoftonline.com/" + _tenantId + "/oauth2/token");

                // Create a list of KVP with the values for the post-body
                List<KeyValuePair<string, string>> authObjects = new List<KeyValuePair<string, string>>();
                authObjects.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                authObjects.Add(new KeyValuePair<string, string>("client_id", _clientId));
                authObjects.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));
                authObjects.Add(new KeyValuePair<string, string>("resource", _resourceId));

                // Convert the authobjects to a FormUrlEncodedContent to add the post-body
                FormUrlEncodedContent content = new FormUrlEncodedContent(authObjects);

                using (client)
                {
                    HttpResponseMessage response = await client.PostAsync(authUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        if (!String.IsNullOrWhiteSpace(result))
                        {
                            var authServerResponse = System.Text.Json.JsonSerializer.Deserialize<AuthServerResponse>(result);
                            return authServerResponse;
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return new AuthServerResponse();
        }
    }

    //public class AuthServerResponse
    //{
    //    [JsonProperty("token_type")]
    //    public string TokenType { get; set; }
    //    [JsonProperty("expires_in")]
    //    public string ExpiresIn { get; set; }
    //    [JsonProperty("ext_expires_in")]
    //    public string ExtExpiresIn { get; set; }
    //    [JsonProperty("expires_on")]
    //    public string ExpiresOn { get; set; }
    //    [JsonProperty("not_before")]
    //    public string NotBefore { get; set; }
    //    [JsonProperty("resource")]
    //    public string Resource { get; set; }
    //    [JsonProperty("access_token")]
    //    public string AccessToken { get; set; }
    //}

    public class AuthServerResponse
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }
    }
}