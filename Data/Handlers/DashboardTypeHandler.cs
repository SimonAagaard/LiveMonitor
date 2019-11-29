using System;
using System.Collections.Generic;
using System.Linq;
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

        //Seed the database with types
        public async Task CreateType()
        {
            //Creates list to be bulk inserted to DB
            List<DashboardType> seedList = new List<DashboardType>();
            List<DashboardType> dashboardTypes = await _dashboardTypeRepo.GetAll();

            //Iterates through the enum "Type" 
            foreach (DashboardType.Type type in (DashboardType.Type[])Enum.GetValues(typeof(DashboardType.Type)))
            {
                //Before adding to the DB, checks to see if the type is already present in the DB.
                bool dashboardExists = dashboardTypes.Any(x => x.DashboardName == type);
                if (!dashboardExists)
                {
                    seedList.Add(new DashboardType()
                    {
                        DashboardTypeId = Guid.NewGuid(),
                        DashboardName = type
                    });
                }
            }
            //Bulk inserts the list to the DB
            await _dashboardTypeRepo.AddMany(seedList);
        }
        public async Task<List<DashboardType>> GetDashboardTypes()
        {
            return await _dashboardTypeRepo.GetAll();
        }
        // Get a single DashboardSetting based on the DashboardSettingId
        public async Task<DashboardType> GetDashboardType(Guid DashboardTypeId)
        {
            return await _dashboardTypeRepo.Get(DashboardTypeId);
        }
    }
}
