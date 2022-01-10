using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
    }
}