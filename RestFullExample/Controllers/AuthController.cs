using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestFullExample.Contracts;
using RestFullExample.Model;
using RestFullExample.Model.Configuration;
using RestFullExample.Model.Dto;

namespace RestFullExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private AuthDetails _authDetails;

        [HttpPost("loginCookie")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDetails)
        {

            var user = _userService.Login(loginDetails.UserName, loginDetails.Password);
            if (user == null)
            {
                ModelState.AddModelError("userName", "Check username or password");
                return BadRequest(ModelState);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("LastConnection", user.LastConnectionTime.ToLongDateString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Cateogory", user.Department),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // create cookie
            await HttpContext.SignInAsync(principal);

            return Ok(new { Message = "Cookie saved" });
        }


        [HttpPost("loginToken")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginToken(LoginDTO loginDetails)
        {

            var user = _userService.Login(loginDetails.UserName, loginDetails.Password);
            if (user == null)
            {
                ModelState.AddModelError("userName", "Check username or password");
                return BadRequest(ModelState);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("LastConnection", user.LastConnectionTime.ToLongDateString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Cateogory", user.Department),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var key = Encoding.ASCII.GetBytes(this._authDetails.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = "Nice",
                Audience = "Mamash nice",
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenHeaderValue = tokenHandler.WriteToken(token);
            return Ok(new { access_token = tokenHeaderValue });


        }
        public AuthController(IUserService userService, IOptions<AuthDetails> options)
        {
            _userService = userService;
            _authDetails = options.Value;
        }

    }

}




