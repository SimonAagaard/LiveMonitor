using Data.Handlers;
using Data.Integrations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class DataSetController
    {
        public async Task<IActionResult> Index()
        {
            IntegrationSettingHandler integrationSettingHandler = new IntegrationSettingHandler();
            AzureConnector conn = new AzureConnector();

            var integrationSetting = await integrationSettingHandler.GetIntegration(new Guid("966d8d9c-6736-4da3-a48a-a9032f512a7a"));
            await conn.GetAzureDataAsync(integrationSetting);

            return new OkResult();
        }
    }
}
