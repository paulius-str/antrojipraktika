using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ServiceContext _serviceContext;

        public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, ServiceContext serviceContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _serviceContext = serviceContext;
        }

        [HttpPost("addstudent")]
        [Authorize]
        public async Task<ActionResult<PersonDto>> AddStudent(RegisterDto registerDto)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            if(CheckUserExistsAsync(registerDto.Username).Result.Value)
            {
                return new BadRequestResult();
            }

            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded) return BadRequest();

            await _serviceContext.Students.AddAsync(new Student{
                AppUserId = user.Id
            });
        
            await _serviceContext.SaveChangesAsync();

            return new PersonDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        [HttpPost("addlecturer")]
        [Authorize]
        public async Task<ActionResult<PersonDto>> AddLecturer(RegisterDto registerDto)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

             if(CheckUserExistsAsync(registerDto.Username).Result.Value)
            {
                return new BadRequestResult();
            }

            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded) return BadRequest();


            await _serviceContext.Lecturers.AddAsync(new Lecturer{
                AppUserId = user.Id
            });
        
            await _serviceContext.SaveChangesAsync();

            return new PersonDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        [HttpDelete("removestudent/{studentId}")]
        [Authorize]
        public async Task<ActionResult<PersonDto>> RemoveStudent(int studentId)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var student = await _serviceContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);

            if(student == null) return BadRequest();

            _serviceContext.Students.Remove(student);
            await _serviceContext.SaveChangesAsync();

            var userToDelete = await _userManager.FindByIdAsync(student.AppUserId);
            await _userManager.DeleteAsync(userToDelete);

            return Ok();
        }

        [HttpDelete("removelecturer/{lecturerId}")]
        [Authorize]
        public async Task<ActionResult<PersonDto>> RemoveLecturer(int lecturerId)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var lecturer = await _serviceContext.Lecturers.FirstOrDefaultAsync(x => x.Id == lecturerId);

            if(lecturer == null) return BadRequest();

            _serviceContext.Lecturers.Remove(lecturer);
            await _serviceContext.SaveChangesAsync();

            var userToDelete = await _userManager.FindByIdAsync(lecturer.AppUserId);
            await _userManager.DeleteAsync(userToDelete);

            return Ok();
        }

        [HttpPost("addsubject")]
        [Authorize]
        public async Task<ActionResult<SubjectDto>> AddSubject(SubjectDto subjectDto)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

           _serviceContext.Subjects.Add(new Subject 
           {
               Name = subjectDto.Name
           });

            await _serviceContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("deletesubject/{id}")]
        [Authorize]
        public async Task<ActionResult<SubjectDto>> DeleteSubject(int id)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var subjectToDelete = await _serviceContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            _serviceContext.Subjects.Remove(subjectToDelete);
            await _serviceContext.SaveChangesAsync();

            return Ok();
        }

       

        [HttpGet("assignedlecturers/{subjectid}")]
        [Authorize]
        public async Task<ActionResult<List<PersonDto>>> GetAssignedLecturers(int subjectId)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var assignations =  _serviceContext.AssignedLecturers.Where(x => x.SubjectId == subjectId);

            List<PersonDto> lecturers = new List<PersonDto>();

            foreach(var assignation in assignations)
            {
                var user = await _userManager.FindByIdAsync(_serviceContext.Lecturers.FirstOrDefaultAsync(x => x.Id == assignation.LecturerId).Result.AppUserId);

                lecturers.Add(new PersonDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = assignation.LecturerId
                });
            }

            return Ok(lecturers);
        }

        [HttpPost("assignedlecturers/remove")]
        [Authorize]
        public async Task<ActionResult> RemoveAssignedLecturer(AssignLecturerDto assignLecturerDto)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var assignations =  _serviceContext.AssignedLecturers.Where(x => x.SubjectId == assignLecturerDto.SubjectId
                                                                                && x.LecturerId == assignLecturerDto.LecturerId);

            foreach(var assignation in assignations)
            {
                _serviceContext.AssignedLecturers.Remove(assignation);
            }

            await _serviceContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("assignedlecturers/add")]
        [Authorize]
        public async Task<ActionResult<List<PersonDto>>> AddAssignedLecturer(AssignLecturerDto assignLecturerDto)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var assignation = new AssignedLecturer 
            {
                LecturerId = assignLecturerDto.LecturerId,
                SubjectId = assignLecturerDto.SubjectId
            };

            await _serviceContext.AssignedLecturers.AddAsync(assignation);

            await _serviceContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("groups")]
        [Authorize]
        public async Task<ActionResult<List<Group>>> GetGroups()
        {      
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            return Ok(await _serviceContext.Groups.ToListAsync());
        }

        [HttpGet("groups/students/{groupId}")]
        [Authorize]
        public async Task<ActionResult<List<PersonDto>>> GetGroupStudents(int groupId)
        {      
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var students = await _serviceContext.Students.Where(x => x.GroupId == groupId).ToListAsync();
            List<PersonDto> result = new List<PersonDto>();

            foreach(var student in students)
            {
                var user = await _userManager.FindByIdAsync(student.AppUserId);

                result.Add(new PersonDto 
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = student.Id
                });
            }

            return Ok(result);
        }

        [HttpDelete("groups/students/remove/{studentId}")]
        [Authorize]
        public async Task<ActionResult> RemoveFromGroup(int studentId)
        {     
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var student = await _serviceContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);

            if(student == null) return BadRequest();

            student.GroupId = null;
            student.Group = null;

            await _serviceContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("groups/students/assigntogroup")]
        [Authorize]
        public async Task<ActionResult<List<PersonDto>>> AssignStudentToGroup(AssignStudentToGroupDto assignStudentToGroupDto)
        {      
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var student = await _serviceContext.Students.FirstOrDefaultAsync(x => x.Id == assignStudentToGroupDto.StudentId);

            if(student == null) return BadRequest();

            student.GroupId = assignStudentToGroupDto.GroupId;
            student.Group = await _serviceContext.Groups.FirstOrDefaultAsync(x => x.Id == assignStudentToGroupDto.GroupId);

            await _serviceContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("assignedsubjects/{groupId}")]
        [Authorize]
        public async Task<ActionResult<List<SubjectDto>>> GetAssignedSubjectsById(int groupId)
        {      
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var assignations = await _serviceContext.AssignedSubjects.Where(x => x.GroupId == groupId).ToListAsync();
            List<SubjectDto> subjects = new List<SubjectDto>();

            foreach(var assignation in assignations)
            {
                var subject = await _serviceContext.Subjects.FirstOrDefaultAsync(x => x.Id == assignation.SubjectId);
                if(subject == null) continue;

                subjects.Add(new SubjectDto
                {
                    Name = subject.Name,
                    Id = subject.Id
                });
            }

            return Ok(subjects);
        }

        [HttpPost("assignedsubjects/delete")]
        [Authorize]
        public async Task<ActionResult> RemoveSubjectAssignment(AssignSubjectToGroupDto assignSubjectToGroupDto)
        {     
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var subject = await _serviceContext.AssignedSubjects.FirstOrDefaultAsync(x => x.GroupId == assignSubjectToGroupDto.GroupId &&
                                                                                        x.SubjectId == assignSubjectToGroupDto.SubjectId);

            if(subject == null) return BadRequest();

            _serviceContext.AssignedSubjects.Remove(subject);   
            await _serviceContext.SaveChangesAsync();    

            return Ok();                                                         
        }

        [HttpPost("assignsubjecttogroup")]
        [Authorize]
        public async Task<ActionResult> AssignSubjectToGroup(AssignSubjectToGroupDto assignSubjectToGroupDto)
        {     
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();

            var subject = new AssignedSubject
            {
                GroupId = assignSubjectToGroupDto.GroupId,
                SubjectId = assignSubjectToGroupDto.SubjectId
            };

            _serviceContext.AssignedSubjects.Add(subject);
            await _serviceContext.SaveChangesAsync();    

            return Ok();                                                         
        }

        private async Task<ActionResult<bool>> CheckUserExistsAsync([FromQuery] string username)
        {
            if(!await _userManager.IsAdmin(HttpContext)) return Unauthorized();
            
            return await _userManager.FindByNameAsync(username) != null;
        }
    }
}