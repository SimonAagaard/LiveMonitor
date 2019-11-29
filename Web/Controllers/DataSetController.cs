using Data.Entities;
using Data.Handlers;
using Data.Integrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class DataSetController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //IntegrationHandler integrationHandler = new IntegrationHandler();
            //IntegrationSettingHandler integrationSettingHandler = new IntegrationSettingHandler();

            //var integration = new Integration
            //{
            //    IntegrationId = Guid.NewGuid(),
            //    IntegrationName = "Azure Test",
            //    IntegrationSettingId = Guid.NewGuid(),
            //    UserId = new Guid("8213517D-670C-EA11-A6A2-C86000C03827"),
            //};

            //await integrationHandler.CreateIntegration(integration);
            //var integrationSetting = new IntegrationSetting
            //{
            //    ClientId = "2de6a64e-eb0c-4275-9952-4ce1d3f0d131",
            //    ClientSecret = "A1l3?[ReU3?L8eEhaYpcUPJG]jEX0_X5",
            //    IntegrationId = integration.IntegrationId,
            //    IntegrationSettingId = integration.IntegrationSettingId,
            //    TenantId = "92404485-d794-4fc2-8d0d-587d30cba2ad",
            //    ResourceId = "https://management.azure.com",
            //    ResourceUrl = @"/subscriptions/2c24d5f6-cb4d-4857-88f8-fe5c9a827f7c/resourceGroups/LiveMonitor/providers/Microsoft.Web/sites/LiveMonitorApp/"
            //};
            //await integrationSettingHandler.CreateIntegrationSetting(integrationSetting);

            IntegrationSettingHandler integrationSettingHandler = new IntegrationSettingHandler();
            AzureConnector conn = new AzureConnector();

            var integrationSetting = await integrationSettingHandler.GetIntegration(new Guid("EA72F6B5-0ECF-49F7-8D0B-D2DEACA627CD"));
            await conn.GetAzureDataAsync(integrationSetting);

            return new OkResult();
        }
    }
}