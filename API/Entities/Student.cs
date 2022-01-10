using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;

namespace API.Entities
{
    public class Student : BaseEntity
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Group? Group { get; set; }
        public System.Nullable<int> GroupId { get; set; }
    }
}