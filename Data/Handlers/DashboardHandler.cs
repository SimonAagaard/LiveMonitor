using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class DashboardHandler
    {
        private readonly Repository<Dashboard> dashboardRepo;

        public DashboardHandler()
        {
            dashboardRepo = new Repository<Dashboard>();
        }

        public async Task CreateDashboard(Dashboard dashboard)
        {
            await dashboardRepo.Add(dashboard);

        }

        // Get all dashboards in the database
        public async Task<List<Dashboard>> GetDashboards()
        {
            return await dashboardRepo.GetAll();
        }

        // Get a single dashboard based on the dashboardId
        public async Task<Dashboard> GetDashboard(Guid dashboardId)
        {
            return await dashboardRepo.Get(dashboardId);
        }

        // Get dashboards based on their userId (DashboardOverview)
        public async Task<List<Dashboard>> GetDashboardsByUserId(Guid userId)
        {
            return await dashboardRepo.GetMany(x => x.UserId == userId);
        }

        // Update a dashboard object
        public async Task UpdateDashboard(Dashboard dashboard)
        {
            await dashboardRepo.Update(dashboard);
        }

        // Hard delete a dashboard based on the dashboardId
        public async Task DeleteDashboard(Dashboard dashboard)
        {
            await dashboardRepo.Delete(dashboard);
        }
    }
}