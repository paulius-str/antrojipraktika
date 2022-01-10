using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;

namespace API.Entities
{
    public class Lecturer : BaseEntity
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}