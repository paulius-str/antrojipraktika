using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class FindAndSetGradeDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int Value { get; set; }
    }
}