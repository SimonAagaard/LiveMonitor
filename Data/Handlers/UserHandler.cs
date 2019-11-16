using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class UserHandler
    {
        private Repository<MonitorUser> userRepo;

        public UserHandler()
        {
            userRepo = new Repository<MonitorUser>();
        }

        public async Task<List<MonitorUser>> GetAllUsers()
        {
            var users = await userRepo.GetAll();
            return users;
        }
    }
}
