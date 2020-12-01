using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context
{
    public class AppUser : IdentityUser<int>
    {
        public string PictureUrl { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public List<Conference> Conferences { get; set; }
        //public List<Submissions> Submissions { get; set; }
    }
}
