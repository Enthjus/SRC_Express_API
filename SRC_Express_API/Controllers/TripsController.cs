using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SRC_Express_API.ModelsInfo;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Controllers
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        private TripsService tripService;
        public TripsController(TripsService _tripService, IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
            tripService = _tripService;
        }

        [HttpGet("findalltripsbysa")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllTripsBySA()
        {
            try
            {
                string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                result = result.Replace(".", "-3T-");
                return Ok(tripService.FindAllTrip());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findtripbyidtrip/{idtrip}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindTripByIDTrip(int idtrip)
        {
            try
            {
                return Ok(tripService.FindTripbyIDTrip(idtrip));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("createtrip")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult CreateTrip([FromBody] TripsInfo tripInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(tripService.CreateTrip(tripInfo));
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

        [HttpPut("updatetrip")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Update([FromBody] TripsInfo tripInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (tripService.DeletePhoto(tripInfo.Id))
                    {
                        return Ok(tripService.UpdateTrip(tripInfo));
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

        [HttpDelete("deletetrip/{idtrip}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DeleteTrip(int idtrip)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (tripService.DeletePhoto(idtrip))
                    {
                        return Ok(tripService.DeleteTrip(idtrip));
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

        [Produces("application/json")]
        [HttpPost("uploadfile")]
        [Authorize(Roles = "SA")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                result = result.Replace(".", "-3T-Trip-");
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

