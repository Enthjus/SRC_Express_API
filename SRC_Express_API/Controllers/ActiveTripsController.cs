using Castle.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRC_Express_API.Models;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Controllers
{
    [Route("api/activetrips")]
    public class ActiveTripsController : Controller
    {
        private ActiveTripService activetripService;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        public ActiveTripsController(ActiveTripService _activetripService, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {

            activetripService = _activetripService;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        [HttpGet("findallactivetrip")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllActiveTrip()
        {
            try
            {

                return Ok(activetripService.FindAllActiveTrips());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findactivetripforpage")]
       // [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindActiveTripForPage()
        {
            try
            {

                return Ok(activetripService.FindActivetripForPage());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findactivetrip/{idactivetrip}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAActiveTrip(int idactivetrip)
        {
            try
            {

                return Ok(activetripService.FindActiveTrip(idactivetrip));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("findallactivetriphasactive")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllActiveTripHasActive()
        {
            try
            {

                return Ok(activetripService.FindAllActiveTripsHasActive());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("findallactivetriphasactive/{idTypecar}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllActiveTripHasActive(int idTypecar)
        {
            try
            {
                var kq = activetripService.FindAllActiveTripsHasActive(idTypecar);
                if (kq == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(kq);
                }
               
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult CreateActiveTrip([FromBody] ActiveTrip activetrip)
        {
            try
            {

                return Ok(activetripService.Create(activetrip));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateactive/{Idactivetrip}")]
        [Produces("application/json")]
        public IActionResult UpdateStatusActivetrip(int Idactivetrip)
        {

            try
            {

                return Ok(activetripService.Update(Idactivetrip));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findallInfoCreate")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllInfoCreate()
        {
            try
            {

                return Ok(activetripService.FindAllInfoCreate());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findalltimestartof1trip/{Idtrip}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindTimeStartOf1Tripp(int Idtrip)
        {
            try
            {

                return Ok(activetripService.FindTimeStartIn1Trip(Idtrip));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
