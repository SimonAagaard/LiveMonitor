using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class IntegrationHandler
    {
        private readonly Repository<Integration> _integrationRepo;

        public IntegrationHandler()
        {
            _integrationRepo = new Repository<Integration>();
        }

        public async Task CreateIntegration(Integration integration)
        {
            await _integrationRepo.Add(integration);
        }

        // Get all Integration in the database
        public async Task<List<Integration>> GetIntegrations()
        {
            return await _integrationRepo.GetAll();
        }

        // Get a single Integration based on the IntegrationId
        public async Task<Integration> GetIntegration(Guid integrationId)
        {
            return await _integrationRepo.Get(integrationId);
        }

        // Get Integrations based on their userid
        public async Task<List<Integration>> GetIntegrationsByUserId(Guid userId)
        {
            return await _integrationRepo.GetMany(x => x.UserId == userId);
        }

        // Update a Integration object
        public async Task UpdateIntegration(Integration integration)
        {
            await _integrationRepo.Update(integration);
        }

        // Hard delete a Integration based on the IntegrationId
        public async Task DeleteIntegration(Integration integration)
        {
            await _integrationRepo.Delete(integration);
        }
    }
}