using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class DashboardGroupHandler
    {
        private readonly Repository<DashboardGroup> _dashboardGroupRepo;

        public DashboardGroupHandler()
        {
            _dashboardGroupRepo = new Repository<DashboardGroup>();
        }

        public async Task CreateDashboardGroup(DashboardGroup DashboardGroup)
        {
            await _dashboardGroupRepo.Add(DashboardGroup);
        }

        // Get all DashboardGroups in the database
        public async Task<List<DashboardGroup>> GetDashboardGroups()
        {
            return await _dashboardGroupRepo.GetAll();
        }
        public async Task<List<DashboardGroup>> GetDashboardGroupsByDashboardId(Guid dashboardId)
        {
            return await _dashboardGroupRepo.GetMany(x => x.DashboardId == dashboardId);
        }
        // Get a single DashboardGroup based on the DashboardGroupId
        public async Task<DashboardGroup> GetDashboardGroup(Guid dashboardGroupId)
        {
            return await _dashboardGroupRepo.Get(dashboardGroupId);
        }

        // Update a DashboardGroup object
        public async Task UpdateDashboardGroup(DashboardGroup dashboardGroup)
        {
            await _dashboardGroupRepo.Update(dashboardGroup);
        }

        // Hard delete a DashboardGroup based on the DashboardGroupId
        public async Task DeleteDashboardGroup(DashboardGroup dashboardGroup)
        {
            await _dashboardGroupRepo.Delete(dashboardGroup);
        }
    }
}
