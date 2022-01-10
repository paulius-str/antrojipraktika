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
    public class StudentController : BaseApiController
    {
        private readonly ServiceContext _serviceContext;
        private readonly UserManager<AppUser> _userManager;

        public StudentController(ServiceContext serviceContext, UserManager<AppUser> userManager)
        {
            _serviceContext = serviceContext;
            _userManager = userManager;
        }

        [HttpGet("grades")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<GradesWithSubjectDto>>> GetGrades()
        {
            var student = await _userManager.GetStudentAsync(HttpContext, _serviceContext);
            if(student == null) return Unauthorized();

            var grades = _serviceContext.Grades.Where(x => x.StudentId == student.Id);

            List<GradesWithSubjectDto> result = new List<GradesWithSubjectDto>();

            foreach(Grade grade in grades){
                var subject = await _serviceContext.Subjects.FirstOrDefaultAsync(x => x.Id == grade.SubjectId);

                if(subject == null) continue;

                result.Add(new GradesWithSubjectDto 
                {
                    subjectName = subject.Name,
                    grade = grade.Value
                });
            }

            return Ok(result);
        }
    }
}