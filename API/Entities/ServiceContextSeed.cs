using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;

namespace API.Entities
{
    public class ServiceContextSeed
    {
        public static async Task SeedSubjects(ServiceContext context) 
        {
            if(!context.Subjects.Any())
            {
                List<Subject> subjects = new List<Subject>()
                {
                    new Subject 
                    {
                        Name = "Objectinis Programavimas"
                    },
                     new Subject 
                    {
                        Name = "Skaitiniai ir optimizavimo metodai"
                    },
                     new Subject 
                    {
                        Name = "Antroji programavimo praktika"
                    },
                     new Subject 
                    {
                        Name = "Vadyba"
                    },
                    new Subject 
                    {
                        Name = "Duomenų bazių projektavimas"
                    },
                    new Subject 
                    {
                        Name = "Informacijos sistemos"
                    }
                };

                foreach(Subject subject in subjects)
                {
                    context.Subjects.AddAsync(subject);
                }

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedAssignments(ServiceContext context) 
        {
            if(!context.Assignments.Any()) 
            {
                List<Assignment> assignments = new List<Assignment>() 
                {
                    new Assignment
                    {
                        Name = "KD",
                        SubjectId = 1,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "EG",
                        SubjectId = 1,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "KD",
                        SubjectId = 2,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "EG",
                        SubjectId = 2,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "KD",
                        SubjectId = 3,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "EG",
                        SubjectId = 3,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "KD",
                        SubjectId = 4,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "EG",
                        SubjectId = 4,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "KD",
                        SubjectId = 5,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "EG",
                        SubjectId = 5,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "KD",
                        SubjectId = 6,
                        Description = " "
                    },
                     new Assignment
                    {
                        Name = "EG",
                        SubjectId = 6,
                        Description = " "
                    }
                };

                foreach(Assignment assignment in assignments)
                {
                    await context.Assignments.AddAsync(assignment);
                }

                await context.SaveChangesAsync();
            }
        }
    }
    
}