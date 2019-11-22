using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class IntegrationSettingHandler
    {
        private readonly Repository<IntegrationSetting> _integrationSettingRepo;

        public IntegrationSettingHandler()
        {
            _integrationSettingRepo = new Repository<IntegrationSetting>();
        }

        public async Task CreateIntegrationSetting(IntegrationSetting integrationSetting)
        {
            await _integrationSettingRepo.Add(integrationSetting);
        }

        // Get all Integration in the database
        public async Task<List<IntegrationSetting>> GetIntegrations()
        {
            return await _integrationSettingRepo.GetAll();
        }

        // Get a single Integration based on the IntegrationId
        public async Task<IntegrationSetting> GetIntegration(Guid integrationSettingId)
        {
            return await _integrationSettingRepo.Get(integrationSettingId);
        }

        // Update a Integration object
        public async Task UpdateIntegration(IntegrationSetting integrationSetting)
        {
            await _integrationSettingRepo.Update(integrationSetting);
        }

        // Hard delete a IntegrationSetting based on the IntegrationSettingId
        public async Task DeleteIntegration(IntegrationSetting integrationSetting)
        {
            await _integrationSettingRepo.Delete(integrationSetting);
        }
    }
}