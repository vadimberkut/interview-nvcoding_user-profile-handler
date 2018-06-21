using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserProfileManager.Data.DbContexts;
using UserProfileManager.Entity.Dto;
using UserProfileManager.Entity.Entities;

namespace UserProfileManager.Data.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext DbContext = null;

        public UserProfileRepository(
               ApplicationDbContext dbContext
           )
        {
            this.DbContext = dbContext;
        }

        public IEnumerable<UserProfileEntity> GetAll(UserProfilesRequestDto data)
        {
            var result = this.DbContext.UserProfiles
                .Skip(data.Count * data.Page)
                .Take(data.Count)
                .ToList();

            return result;
        }

        public UserProfileEntity Get(Guid id)
        {
            var result = this.DbContext.UserProfiles
                .Single(x => x.Id == id);
            return result;
        }

        public UserProfileEntity Create(UserProfileEntity data)
        {
            var result = this.DbContext.UserProfiles
               .Add(data);
            return result.Entity;
        }

        public UserProfileEntity Update(UserProfileEntity data)
        {
            var result = this.DbContext.UserProfiles
                  .Update(data);
            return result.Entity;
        }

        public UserProfileEntity UpdateRole()
        {
            throw new NotImplementedException();
        }
    }
}
