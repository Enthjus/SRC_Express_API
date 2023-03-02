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
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        private CarsService carsService;
        public CarsController(CarsService _carsService, IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
            carsService = _carsService;
        }

        [HttpGet("findallcarsbysa")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllCarsBySA()
        {
            try
            {
                string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                result = result.Replace(".", "-3T-");
                return Ok(carsService.FindAllCars());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findcarbyid/{idcar}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindCarsByID(string idcar)
        {
            try
            {
                string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                result = result.Replace(".", "-3T-");
                return Ok(carsService.FindCarbyID(idcar));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findtypecarbyid/{id}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindTypecarByID(int id)
        {
            try
            {
                return Ok(carsService.FindTypeCarByIDtype(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("createcar")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult CreateCar([FromBody] CarsInfo carInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = Path.GetRandomFileName();  // tao chuoi ngau nhien ten file
                    result = result.Replace(".", "-3T-");


                    return Ok(carsService.CreateCars(carInfo));
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

        [HttpPut("updatecar")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Update([FromBody] CarsInfo carInfo)
        {
            Debug.WriteLine(carInfo.Id);
            try
            {
                if (ModelState.IsValid)
                {
                    if (carsService.DeletePhoto(carInfo.Id))
                    {
                        return Ok(carsService.UpdateCars(carInfo));
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

        [HttpDelete("deletecar/{idcar}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DeleteCars(string idcar)
        {

            try
            {
                if (carsService.DeletePhoto(idcar))
                {
                    return Ok(carsService.DeleteCars(idcar));
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
                result = result.Replace(".", "-3T-Car-");
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
