using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AssignedLecturer : BaseEntity
    {
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public Lecturer Lecturer { get; set; }
        public int LecturerId { get; set; }
    }
}