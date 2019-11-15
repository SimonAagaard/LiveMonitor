using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDashboardRepository : IRepository<Dashboard>
    {
        Task<List<Dashboard>> GetDashboardsForUser(string userId);
        Task<Dashboard> GetSpecificDashboardForUser(Guid dashboardId, string userId);
        Task AddDashboard(Dashboard dashboard);
        Task DeleteDashboard(Guid dashboardId, string userId);
    }
}
