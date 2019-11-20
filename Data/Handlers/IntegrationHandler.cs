using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    class IntegrationHandler
    {
        private readonly Repository<Integration> integrationRepo;

        public IntegrationHandler()
        {
            integrationRepo = new Repository<Integration>();
        }

        public async Task CreateIntegration(Integration integration)
        {
            await integrationRepo.Add(integration);
        }

        // Get all Integration in the database
        public async Task<List<Integration>> GetIntegrations()
        {
            return await integrationRepo.GetAll();
        }

        // Get a single Integration based on the IntegrationId
        public async Task<Integration> GetIntegration(Guid integrationId)
        {
            return await integrationRepo.Get(integrationId);
        }

        // Get Integrations based on their userid
        public async Task<List<Integration>> GetIntegrationsByIntegrationId(Guid userId)
        {
            return await integrationRepo.GetMany(x => x.UserId == userId);
        }

        // Update a Integration object
        public async Task UpdateIntegration(Integration integration)
        {
            await integrationRepo.Update(integration);
        }

        // Hard delete a Integration based on the IntegrationId
        public async Task DeleteIntegration(Guid integrationId)
        {
            await integrationRepo.Delete(integrationId);
        }
    }
}
