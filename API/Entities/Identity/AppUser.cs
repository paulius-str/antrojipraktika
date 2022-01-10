using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLecturer { get; set; }
    }
}