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

            var integrationSetting = await integrationSettingHandler.GetIntegration(new Guid("854C79B2-6B31-472C-938F-F1077343AAC0"));
            await conn.GetAzureDataAsync(integrationSetting);

            return new OkResult();
        }
    }
}
