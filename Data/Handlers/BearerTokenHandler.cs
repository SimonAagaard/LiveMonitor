using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{

    public class BearerTokenHandler
    {
        private readonly IRepository<BearerToken> _bearerTokenRepo;

        public BearerTokenHandler()
        {
            _bearerTokenRepo = new Repository<BearerToken>();
        }

        // Create a new bearertoken
        public async Task CreateBearerToken(BearerToken bearerToken)
        {
            await _bearerTokenRepo.Add(bearerToken);
        }

        // Get all bearerTokens in the database
        public async Task<List<BearerToken>> GetBearerTokens()
        {
            return await _bearerTokenRepo.GetAll();
        }

        // Get a single bearerToken based on the integrationSettingId and datetime
        public async Task<BearerToken> GetValidBearerToken(Guid integrationSettingId, DateTime dateNow)
        {
            return await _bearerTokenRepo.Get(x => x.IntegrationSettingId == integrationSettingId && x.DateExpired > dateNow);
        }

        // Get valid bearertokens based on integrationSettingId and datetime
        public async Task<List<BearerToken>> GetValidBearerTokens(Guid integrationSettingId, DateTime dateNow)
        {
            return await _bearerTokenRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId && x.DateExpired < dateNow);
        }

        // Get all bearertokens by a integrationSettingId and a timespan.
        public async Task<List<BearerToken>> GetBearerTokenByDates(Guid integrationSettingId, DateTime fromDate, DateTime toDate)
        {
            return await _bearerTokenRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId && x.DateCreated >= fromDate && x.DateCreated <= toDate);
        }

        // Update a bearerToken object
        public async Task UpdateBearerToken(BearerToken bearerToken)
        {
            await _bearerTokenRepo.Update(bearerToken);
        }

        // Hard delete a bearerToken based on the bearerTokenId
        public async Task DeleteBearerToken(BearerToken bearerToken)
        {
            await _bearerTokenRepo.Delete(bearerToken);
        }
    }
}