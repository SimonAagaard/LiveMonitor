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
    public class DataSetController : Controller
    {
        private readonly IntegrationSettingHandler _integrationSettingHandler;
        private readonly DataSetHandler _dataSetHandler;

        public DataSetController()
        {
            _integrationSettingHandler = new IntegrationSettingHandler();
            _dataSetHandler = new DataSetHandler();
        }

        // Create all datasets for current integrationSettings
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<IntegrationSetting> integrations = await _integrationSettingHandler.GetIntegrationSettings();

            // Create DataSets for all integrations thats active
            foreach (IntegrationSetting integrationSetting in integrations.Where(x => x.IsActive == true))
            {
                if (!String.IsNullOrWhiteSpace(integrationSetting.TenantId) && 
                    !String.IsNullOrWhiteSpace(integrationSetting.ClientId) && 
                    !String.IsNullOrWhiteSpace(integrationSetting.ClientSecret) &&
                    !String.IsNullOrWhiteSpace(integrationSetting.ResourceId) &&
                    !String.IsNullOrWhiteSpace(integrationSetting.ResourceUrl))
                {
                    AzureConnector azureConnector = new AzureConnector(integrationSetting);
                    await azureConnector.GetAzureDataAsync();
                }
            }

            return new OkResult();
        }

        public async Task<JsonResult> GetDataSet(Guid integrationSettingId)
        {
            if (integrationSettingId != Guid.Empty)
            {
                var dataSet = await _dataSetHandler.GetNewestDataSetByIntegrationSettingId(integrationSettingId);

                if (dataSet != null)
                {
                    return Json(dataSet);
                }
            }

            return Json("");
        }

        public async Task<JsonResult> GetAmountOfDataSets(Guid integrationSettingId, int amountOfDataSets)
        {
            if (integrationSettingId != Guid.Empty)
            {
                List<DataSet> dataSets = await _dataSetHandler.GetCertainAmountOfDataSets(integrationSettingId, amountOfDataSets);

                if (dataSets.Count > 0)
                {
                    return Json(dataSets);
                }
            }

            return Json("");
        }
    }
}