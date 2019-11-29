using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class DashboardSettingHandler
    {
        private readonly Repository<DashboardSetting> DashboardSettingRepo;

        public DashboardSettingHandler()
        {
            DashboardSettingRepo = new Repository<DashboardSetting>();
        }

        public async Task CreateDashboardSetting(DashboardSetting DashboardSetting)
        {
            await DashboardSettingRepo.Add(DashboardSetting);
        }

        // Get all DashboardSettings in the database
        public async Task<List<DashboardSetting>> GetDashboardSettings()
        {
            return await DashboardSettingRepo.GetAll();
        }

        // Get a single DashboardSetting based on the DashboardSettingId
        public async Task<DashboardSetting> GetDashboardSetting(Guid DashboardSettingId)
        {
            return await DashboardSettingRepo.Get(DashboardSettingId);
        }

        // Update a DashboardSetting object
        public async Task UpdateDashboardSetting(DashboardSetting DashboardSetting)
        {
            await DashboardSettingRepo.Update(DashboardSetting);
        }

        // Hard delete a DashboardSetting based on the DashboardSettingId
        public async Task DeleteDashboardSetting(DashboardSetting dashboardSetting)
        {
            await DashboardSettingRepo.Delete(dashboardSetting);
        }
    }
}