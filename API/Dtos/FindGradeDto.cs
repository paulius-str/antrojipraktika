using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class FindGradeDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}