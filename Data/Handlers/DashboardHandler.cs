using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Handlers
{
    class DashboardHandler
    {
        private Repository<Dashboard> repo;

        public DashboardHandler()
        {
            repo = new Repository<Dashboard>();
        }

        // TODO
        public async void GetDashboards()
        {
            List<Dashboard> dashboards = await repo.GetAll();
        }
    }
}
