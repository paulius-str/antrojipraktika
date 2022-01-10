using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AssignedSubject : BaseEntity
    {
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
    }
}