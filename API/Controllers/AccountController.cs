using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities.Identity;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ServiceContext _serviceContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, ServiceContext serviceContext)
        {
            this._serviceContext = serviceContext;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var username = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            return new UserDto
            {
                DisplayName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if(user == null) return Unauthorized();

            switch (loginDto.UserType.ToLower())
            {
                case "admin":
                    if(!user.IsAdmin)
                    {
                        return Unauthorized();
                    }
                    break;
                case "lecturer":
                    if(_serviceContext.Lecturers.FirstOrDefault(x => x.AppUserId == user.Id) == null)
                    {
                        return Unauthorized();
                    }
                    break;
                case "student":
                    if(_serviceContext.Students.FirstOrDefault(x => x.AppUserId == user.Id) == null)
                    {
                        return Unauthorized();
                    }
                    break;
                default:
                        return Unauthorized();
                    break;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized();

            return new UserDto {
                Token = _tokenService.CreateToken(user),
                DisplayName = user.FirstName
            };
        }
    }
}