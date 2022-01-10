using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
    }
}