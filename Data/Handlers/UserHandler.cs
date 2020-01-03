using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public class UserHandler
    {
        private readonly IRepository<MonitorUser> userRepo;

        public UserHandler()
        {
            userRepo = new Repository<MonitorUser>();
        }

        // Get all users in the database
        public async Task<List<MonitorUser>> GetUsers()
        {
            return await userRepo.GetAll();
        }

        // Get a single user based on userID
        public async Task<MonitorUser> GetUser(Guid userId)
        {
            return await userRepo.Get(userId);
        }

        // Build out repository as needed with similar methods to make custom Gets/queries
        // Get all active users in the database based on IsDeleted
        public async Task<List<MonitorUser>> GetActiveUsers()
        {
            return await userRepo.GetMany(u => u.IsDeleted == false);
        }

        // Update a user object
        public async Task UpdateUser(MonitorUser user)
        {
            await userRepo.Update(user);
        }

        // Hard delete a user based on the userId
        public async Task DeleteUser(MonitorUser monitorUser)
        {
            await userRepo.Delete(monitorUser);
        }
    }
}