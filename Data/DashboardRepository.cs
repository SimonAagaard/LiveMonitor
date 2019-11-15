using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbContext _context;

        public DashboardRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Dashboard>> GetAll()
        {
            return await _context.Dashboards.ToListAsync();
        }

        public async Task<List<Dashboard>> GetDashboardsForUser(string userId)
        {
            var results = await _context.Dashboards
                .Where(d => d.UserId == userId).ToListAsync();
            return results;
        }

        public async Task<Dashboard> GetSpecificDashboardForUser(Guid dashboardId, string userId)
        {
            var result = await _context.Dashboards
                .Where(d => d.DashboardId == dashboardId)
                .Where(d => d.UserId == userId).FirstOrDefaultAsync();
            return result;
        }

        public async Task AddDashboard(Dashboard dashboard)
        {
            await _context.Dashboards.AddAsync(dashboard);
            _context.SaveChanges();
        }

        public async Task DeleteDashboard(Guid dashboardId, string userId)
        {
            var dashboard = await _context.Dashboards
                .Where(d => d.DashboardId == dashboardId)
                .Where(d => d.UserId == userId).FirstOrDefaultAsync();

            _context.Dashboards.Remove(dashboard);
            _context.SaveChanges();
        }

        public Task<Dashboard> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Dashboard> Add(Dashboard entity)
        {
            throw new NotImplementedException();
        }

        public Task<Dashboard> Update(Dashboard entity)
        {
            throw new NotImplementedException();
        }

        public Task<Dashboard> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
