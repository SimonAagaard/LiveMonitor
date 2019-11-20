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
        private readonly string _clientId = "1cac3850-6566-4ed5-b3a0-22ad3570113f";
        private readonly string _clientSecret = "NNiuUDyGfU1-6t3MPzC_8[:BtagP1AX";
        private readonly string _tenantId = "711dd56b-bdb2-48ce-s270-3b1db97f5d17";

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
                authObjects.Add(new KeyValuePair<string, string>("Resource", "https://graph.microsoft.com"));

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
                            var authServerResponse = JsonSerializer.Deserialize<AuthServerResponse>(result);
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
    public class AuthServerResponse
    {
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public int ExtExpiresIn { get; set; }
        public string AccessToken { get; set; }
    }
}