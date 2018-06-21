using System;
using System.Collections.Generic;
using System.Text;

namespace UserProfileManager.Entity.Dto
{
    public class UserProfilesRequestDto
    {
        public int Count { get; set; } = 10;
        public int Page { get; set; } = 0;
        public string SearchText { get; set; }
        public bool? Enabled { get; set; }
    }
}
