using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserProfileManager.Data.Repositories.Base;
using UserProfileManager.Entity.Dto;

namespace UserProfileManager.Controllers
{
    [Route("api/[controller]")]
    public class UserProfilesController : BaseApiController
    {
        private readonly IUnitOfWork UnitOfWork = null;

        public UserProfilesController(
            IUnitOfWork unitOfWork
        )
        {
            this.UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<string> Test()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]UserProfilesRequestDto data)
        {
            var result = this.UnitOfWork.UserProfileRepository.GetAll(data);
            return this.JsonResponse(result);
        }
    }
}
