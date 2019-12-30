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
        private readonly Repository<Dashboard> _dashboardRepo;

        public DashboardHandler()
        {
            _dashboardRepo = new Repository<Dashboard>();
        }

        public async Task CreateDashboard(Dashboard dashboard)
        {
            await _dashboardRepo.Add(dashboard);

        }

        // Get all dashboards in the database
        public async Task<List<Dashboard>> GetDashboards()
        {
            return await _dashboardRepo.GetAll();
        }

        // Get a single dashboard based on the dashboardId
        public async Task<Dashboard> GetDashboard(Guid dashboardId)
        {
            return await _dashboardRepo.Get(dashboardId);
        }

        //Get a single dashboard along with its setting based on its dashboardId
        public async Task<Dashboard> GetDashBoardAndDashboardSetting(Guid dashboardId, string[] children)
        {
            return await _dashboardRepo.Get(x => x.DashboardId == dashboardId, children);
        }

        //Get a list of dashboards along with their their settings for a given user based on the provided userId
        public async Task<List<Dashboard>> GetDashboardsAndDashboardSettings(Guid userId, string[] children)
        {
            return await _dashboardRepo.GetMany(x => x.UserId == userId, children);
        }

        // Get dashboards based on their userId (DashboardOverview)
        public async Task<List<Dashboard>> GetDashboardsByUserId(Guid userId)
        {
            return await _dashboardRepo.GetMany(x => x.UserId == userId);
        }

        // Update a dashboard object
        public async Task UpdateDashboard(Dashboard dashboard)
        {
            await _dashboardRepo.Update(dashboard);
        }

        // Hard delete a dashboard based on the dashboardId
        public async Task DeleteDashboard(Dashboard dashboard)
        {
            await _dashboardRepo.Delete(dashboard);
        }
    }
}