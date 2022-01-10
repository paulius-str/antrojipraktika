using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class DataController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ServiceContext _serviceContext;

        public DataController(UserManager<AppUser> userManager, ServiceContext serviceContext)
        {
            _userManager = userManager;
            _serviceContext = serviceContext;
        }

        [HttpGet("subjects")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Subject>>> GetSubjects() 
        {
             if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();
             
            return Ok(_serviceContext.Subjects.ToListAsync());
        }

        [HttpGet("subject/{id}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Subject>>> GetSubject(int id) 
        {
             if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            return Ok(_serviceContext.Subjects.Where(x => x.Id == id));
        }

        [HttpGet("assignments/{subjectId}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Assignment>>> GetAssignments(int subjectId)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            return Ok(_serviceContext.Assignments.Where(x => x.SubjectId == subjectId));
        }

       

        [HttpGet("students")]
        [Authorize]
        public async Task<ActionResult<List<PersonDto>>> GetStudents()
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var students = await _serviceContext.Students.ToListAsync();

            List<PersonDto> listToReturn = new List<PersonDto>();

            foreach(Student student in students)
            {
                var user = await _userManager.FindByIdAsync(student.AppUserId);

                listToReturn.Add(new PersonDto 
                {
                    Id = student.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }

            return Ok(listToReturn);
        }

    

        [HttpGet("lecturers")]
        [Authorize]
        public async Task<ActionResult<List<PersonDto>>> GetLecturers()
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var list = await _serviceContext.Lecturers.ToListAsync();
            List<PersonDto> listToReturn = new List<PersonDto>();

            foreach(Lecturer user in list)
            {
                AppUser appUser = await _userManager.FindByIdAsync(user.AppUserId);
                
                listToReturn.Add(new PersonDto 
                {
                    Id = user.Id,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName
                });
            }

            return Ok(listToReturn);
        }
    }
}