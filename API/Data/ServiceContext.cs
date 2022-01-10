using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ServiceContext : IdentityDbContext<AppUser>
    {
        public ServiceContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<AssignedSubject> AssignedSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<AssignedLecturer> AssignedLecturers { get; set; }
    }
}