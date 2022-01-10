using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserTypeExtensions
    {
        public static async Task<Lecturer> GetLecturerAsync(this UserManager<AppUser> userManager, HttpContext context, ServiceContext serviceContext)
        {
            var username = context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByNameAsync(username);

            Lecturer lecturer = await serviceContext.Lecturers.FirstOrDefaultAsync(x => x.AppUserId == user.Id);

            return lecturer;
        }

        public static async Task<Student> GetStudentAsync(this UserManager<AppUser> userManager, HttpContext context, ServiceContext serviceContext)
        {
            var username = context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByNameAsync(username);

            Student student = await serviceContext.Students.FirstOrDefaultAsync(x => x.AppUserId == user.Id);

            return student;
        }

        public static async Task<bool> IsAdmin(this UserManager<AppUser> userManager, HttpContext context)
        {
            var username = context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByNameAsync(username);

            return user.IsAdmin;
        }
    }
}