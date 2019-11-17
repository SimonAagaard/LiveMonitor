using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Integrations
{
    class AzureConnector
    {
        private readonly string _clientId = "";
        private readonly string _clientSecret = "";
        private readonly string _tenantId = "";

        public AzureConnector(string clientId, string clientSecret, string tenantId)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tenantId = tenantId;
        }
    }
}