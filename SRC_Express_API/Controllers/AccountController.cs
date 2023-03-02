
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SRC_Express_API.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private AccountService accountService;
        private DashboardService dashboardService;
        private IConfiguration configuration;
        private LoginService loginService;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        public AccountController(DashboardService _dashboardService, AccountService _accountService, IConfiguration _configuration, LoginService _loginService, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            configuration = _configuration;
            accountService = _accountService;
            dashboardService = _dashboardService;
            loginService = _loginService;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] AccountInfo acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("zzzz");
                    var user = loginService.Login(acc);
                    if (user != null)
                    {

                        var roles = user.NameRole;
                        var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("Fullname", user.FullName.ToString()),

                        new Claim(ClaimTypes.Role,roles )
                    };


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: configuration["Jwt:Issuer"],
                            audience: configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddHours(5),
                            signingCredentials: signIn);
                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    else
                    {
                        return Ok("Invaid creadentials");
                    }

                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult DashBoard()
        {
            //try
            //{

                return Ok(dashboardService.Statistical());
            //}
            //catch
            //{
            //    return BadRequest();
            //}
        }

        [HttpGet("findallaccountbysa")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllAccountBySA()
        {
            try
            {
                string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                result = result.Replace(".", "-3T-");
                return Ok(accountService.FindAllAccountBySA());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findidaccountbysa/{idacc}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindIdAccountBySA(string idacc)
        {
            try
            {

                return Ok(accountService.FindIDAccountBySA(idacc));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findallaccountbyadmin")]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        public IActionResult FindAllAccountByAdmin()
        {
            try
            {

                return Ok(accountService.FindAllAccountByAdmin());

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Register([FromBody] AccountInfo acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Debug.WriteLine(acc.Username);
                    return Ok(loginService.Register(acc));
                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Update([FromBody] AccountInfo acc)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (accountService.DeletePhoto(acc.Id))
                    {
                        return Ok(accountService.UpdateAccount(acc));
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{idacc}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DeleteAccount(string idacc)
        {

            try
            {

                if (accountService.DeletePhoto(idacc))
                {
                    return Ok(accountService.DeleteAccount(idacc));
                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("uploadfile")]
        [Authorize(Roles = "SA")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                result = result.Replace(".", "-3T-");
                var filename = result + file.ContentType.Replace("/", ".");
                var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var baseURL = httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host + httpContextAccessor.HttpContext.Request.PathBase;
                return Ok(baseURL + "/uploads/" + filename);
            }
            catch
            {
                return BadRequest();
            }
        }





    }
}
