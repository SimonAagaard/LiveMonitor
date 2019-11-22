using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Handlers
{
    public class DashboardTypeHandler
    {
        private readonly Repository<DashboardType> _dashboardTypeRepo;
        private static readonly DashboardTypeHandler _instance = new DashboardTypeHandler();
        public DashboardTypeHandler()
        {
            _dashboardTypeRepo = new Repository<DashboardType>();
        }
        public static DashboardTypeHandler Instance
        {
            get { return _instance; }
        }
        public async Task CreateType()
        {
            IList<DashboardType> dashboardTypes = new List<DashboardType>();
            dashboardTypes.Add(new DashboardType()
            {
                DashboardTypeId = Guid.NewGuid(),
                DashboardName = DashboardType.Type.LineChart
                
            });
            dashboardTypes.Add(new DashboardType()
            {
                DashboardTypeId = Guid.NewGuid(),
                DashboardName = DashboardType.Type.AreaChart
            });
         
            foreach (DashboardType dashboardType in dashboardTypes)
            {
                await _dashboardTypeRepo.Seed(dashboardType);
            }

        }
    }
}
