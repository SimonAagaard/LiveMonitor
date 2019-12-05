﻿using Data.Entities;
using Data.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Data.Integrations
{
    public class AzureConnector
    {
        private readonly IntegrationSetting _integrationSetting;

        public AzureConnector(IntegrationSetting integrationSetting)
        {
            _integrationSetting = integrationSetting;
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
                Uri authUrl = new Uri("https://login.microsoftonline.com/" + _integrationSetting.TenantId + "/oauth2/token");

                // Create a list of KVP with the values for the post-body
                List<KeyValuePair<string, string>> authObjects = new List<KeyValuePair<string, string>>();
                authObjects.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                authObjects.Add(new KeyValuePair<string, string>("client_id", _integrationSetting.ClientId));
                authObjects.Add(new KeyValuePair<string, string>("client_secret", _integrationSetting.ClientSecret));
                authObjects.Add(new KeyValuePair<string, string>("resource", _integrationSetting.ResourceId));

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

        // Get datasets from Azure based on the passed integrationsettings and values passed to the constructor
        public async Task<AzureDataResponse> GetAzureDataAsync()
        {
            // Set datetime for the request to the API
            string currentTime = DateTime.UtcNow.ToString("o");
            string fromTime = DateTime.UtcNow.AddMinutes(-2).ToString("o");
            DataSetHandler dataSetHandler = new DataSetHandler();

            // Get a valid bearertoken either from the DB or new from the API
            string accessToken = await GetAzureBearerTokenAsync();

            // Split url to support new integrations
            Uri Url = new Uri(@"https://management.azure.com" + _integrationSetting.ResourceUrl + "providers/microsoft.insights/metrics?" +
                "timespan=" + fromTime + @"/" + currentTime +
                "&interval=PT1M" +
                "&aggregation=Average" +
                "&metricnames=AverageResponseTime" +
                "&api-version=2018-01-01" +
                "&metricnamespace=Microsoft.Web/sites");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Send request to the API for metrics
                HttpResponseMessage response = await client.GetAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response to a string
                    string result = await response.Content.ReadAsStringAsync();

                    // Deserialize the response from json to an object
                    AzureDataResponse azureDataResponse = System.Text.Json.JsonSerializer.Deserialize<AzureDataResponse>(result);

                    // Iterate the objects in the response to get to the metrics returned
                    if (azureDataResponse != null)
                    {
                        foreach (var value in azureDataResponse.value)
                        {
                            foreach (var timeseries in value.timeseries)
                            {
                                // Sort list by highest datetime value and select the newest object
                                timeseries.data.Sort((x, y) => y.timeStamp.CompareTo(x.timeStamp));
                                Metric newestMetric = timeseries.data.FirstOrDefault();

                                DataSet dataSet = new DataSet
                                {
                                    DataSetId = Guid.NewGuid(),
                                    DateCreated = DateTime.UtcNow,
                                    IntegrationSettingId = _integrationSetting.IntegrationSettingId,
                                    XValue = newestMetric.timeStamp.ToUniversalTime(),
                                    YValue = Math.Round(newestMetric.average * 1000, 2) // Save in milliseconds - Total for count, average for average, etc.
                                };

                                // Check if the dataset already exists - Create new if it doesn't
                                // TODO - use bulk create 
                                var dataSetCheck = await dataSetHandler.GetDataSetByIntegrationSettingIdAndTimestamp(_integrationSetting.IntegrationSettingId, dataSet.XValue);
                                if (dataSetCheck == null)
                                {
                                    await dataSetHandler.CreateDataSet(dataSet);
                                }
                            }
                        }
                    }
                }
            }

            return new AzureDataResponse();
        }

        // Helper method to check for valid bearer token. Creates new if no valid is found, returns existing if its valid
        public async Task<string> GetAzureBearerTokenAsync()
        {
            // instantiate a bearertokenhandler
            BearerTokenHandler bearerTokenHandler = new BearerTokenHandler();

            // Attempt to get a valid bearerToken from the Db
            BearerToken bearerToken = await bearerTokenHandler.GetValidBearerToken(_integrationSetting.IntegrationSettingId, DateTime.UtcNow);

            if (!String.IsNullOrWhiteSpace(bearerToken?.AccessToken))
            {
                return bearerToken.AccessToken;
            }
            else
            {
                // If not valid BearerToken exists in the database, create a new one and set expiry time to 59 minutes to ensure we don't use an expired one
                AuthServerResponse authResponse = await GetAuthTokenAsync();
                await bearerTokenHandler.CreateBearerToken(new BearerToken
                {
                    AccessToken = authResponse.access_token,
                    BearerTokenId = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow,
                    DateExpired = DateTime.UtcNow.AddMinutes(59),
                    IntegrationSettingId = _integrationSetting.IntegrationSettingId
                });
                return authResponse.access_token;
            }
        }
    }

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

    // Data response
    public class Name
    {
        public string value { get; set; }
        public string localizedValue { get; set; }
    }

    public class Metric
    {
        public DateTime timeStamp { get; set; }
        public double total { get; set; }
        public double average { get; set; }
    }

    [JsonObject("timeseries")]
    public class Timesery
    {
        public List<object> metadatavalues { get; set; }
        public List<Metric> data { get; set; }
        public Timesery()
        {
            data = new List<Metric>();
        }
    }

    public class AzureData
    {
        public string id { get; set; }
        public string type { get; set; }
        public Name name { get; set; }
        public string displayDescription { get; set; }
        public string unit { get; set; }
        public List<Timesery> timeseries { get; set; }
        public string errorCode { get; set; }
        public AzureData()
        {
            timeseries = new List<Timesery>();
        }
    }

    public class AzureDataResponse
    {
        public int cost { get; set; }
        public string timespan { get; set; }
        public string interval { get; set; }
        public List<AzureData> value { get; set; }
        public string stringNamespace { get; set; }
        public string resourceregion { get; set; }
        public AzureDataResponse()
        {
            value = new List<AzureData>();
        }
    }
}