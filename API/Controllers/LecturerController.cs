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
    public class LecturerController : BaseApiController
    {
        private readonly ServiceContext _serviceContext;
        private readonly UserManager<AppUser> _userManager;

        public LecturerController(ServiceContext serviceContext, UserManager<AppUser> userManager)
        {
            _serviceContext = serviceContext;
            _userManager = userManager;
        }

        [HttpGet("getsubjects")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<SubjectDto>>> GetSubjects()
        {
            var lecturer = await _userManager.GetLecturerAsync(HttpContext, _serviceContext);
            if(lecturer == null) return Unauthorized();

            var assignedSubjects = _serviceContext.AssignedLecturers.Where(x => x.LecturerId == lecturer.Id);
            List<SubjectDto> subjects = new List<SubjectDto>();

            foreach(var assignedSubject in assignedSubjects)
            {
                var subject = await _serviceContext.Subjects.FirstOrDefaultAsync(x => x.Id == assignedSubject.SubjectId);

                if(subject == null) continue;

                subjects.Add(new SubjectDto 
                {
                    Name = subject.Name,
                    Id = subject.Id
                });
            }

            return Ok(subjects);
        }

        [HttpGet("getstudents/{subjectId}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<PersonDto>>> GetStudents(int subjectId)
        {
             var lecturer = await _userManager.GetLecturerAsync(HttpContext, _serviceContext);
            if(lecturer == null) return Unauthorized();

            var assignedSubject = await _serviceContext.AssignedSubjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);
            var group = await _serviceContext.Groups.FirstOrDefaultAsync(x => x.Id == assignedSubject.GroupId);
            var students = _serviceContext.Students.Where(x => x.GroupId == group.Id);

            var studentsDtos = new List<PersonDto>();

            foreach(var student in students)
            {
                var user = await _userManager.FindByIdAsync(student.AppUserId);

                studentsDtos.Add(new PersonDto{
                    Id = student.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }

            return Ok(studentsDtos);
        }

        [HttpPost("getgrade")]
        [Authorize]
        public async Task<ActionResult<GradeDto>> GetGrade(FindGradeDto findGradeDto)
        {
            var lecturer = await _userManager.GetLecturerAsync(HttpContext, _serviceContext);
            if(lecturer == null) return Unauthorized();

            if(findGradeDto == null) return BadRequest();

            var grade = await _serviceContext.Grades.FirstOrDefaultAsync(
                x => x.StudentId == findGradeDto.StudentId && x.SubjectId == findGradeDto.SubjectId
            );

            if(grade == null)
            {
                grade = new Grade
                {
                    Value = -1,
                    StudentId = findGradeDto.StudentId,
                    SubjectId = findGradeDto.SubjectId
                };

                await _serviceContext.AddAsync<Grade>(grade);
                await _serviceContext.SaveChangesAsync();
            }

            var gradeDto = new GradeDto 
            {
                Value = grade.Value,
                StudentId = grade.StudentId,
                SubjectId = grade.SubjectId
            };

            return Ok(gradeDto);
        }

        [HttpPut("setgrade")]
        public async Task<ActionResult<PersonDto>> SetGrade(FindAndSetGradeDto findGradeDto)
        {
            var lecturer = await _userManager.GetLecturerAsync(HttpContext, _serviceContext);
            if(lecturer == null) return Unauthorized();

            if(findGradeDto == null) return BadRequest();

            var grade = await _serviceContext.Grades.FirstOrDefaultAsync(
                x => x.StudentId == findGradeDto.StudentId && x.SubjectId == findGradeDto.SubjectId
            );

            grade.Value = Math.Clamp(findGradeDto.Value, 1, 10);

            await _serviceContext.SaveChangesAsync();

            return Ok();
        }
    }
}