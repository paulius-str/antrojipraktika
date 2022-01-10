using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                List<AppUser> usersToAdd = new List<AppUser> 
                {
                new AppUser
                {
                    FirstName = "Lecturer",
                    LastName = "1",
                    UserName = "Lecturer1",
                    IsLecturer = true
                },
                new AppUser
                {
                    FirstName = "Lecturer",
                    LastName = "2",
                    UserName = "Lecturer2",
                    IsLecturer = true
                },
                new AppUser
                {
                    FirstName = "Lecturer",
                    LastName = "3",
                    UserName = "Lecturer3",
                    IsLecturer = true
                },
                   new AppUser
                {
                    FirstName = "Lecturer",
                    LastName = "4",
                    UserName = "Lecturer4",
                    IsLecturer = true
                },

                new AppUser
                {
                    FirstName = "Lecturer",
                    LastName = "5",
                    UserName = "Lecturer5",
                    IsLecturer = true
                }
                
                };        

                foreach(AppUser user in usersToAdd)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }              
                
            }
        }
    }
}