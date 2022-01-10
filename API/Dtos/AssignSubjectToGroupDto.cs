using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AssignSubjectToGroupDto
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }
}