using System;
using System.Collections.Generic;
using System.Text;
using UserProfileManager.Entity.Dto;
using UserProfileManager.Entity.Entities;

namespace UserProfileManager.Data.Repositories
{
    public interface IUserProfileRepository
    {
        IEnumerable<UserProfileEntity> GetAll(UserProfilesRequestDto data);
        UserProfileEntity Get(Guid id);
        UserProfileEntity Create(UserProfileEntity data);
        UserProfileEntity Update(UserProfileEntity data);
        UserProfileEntity UpdateRole();
        //UserProfileEntity ToggleUserEnabled();
    }
}
