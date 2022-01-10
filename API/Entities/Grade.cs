using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;

namespace API.Entities
{
    public class Grade : BaseEntity
    {
        public int Value { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}