using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRC_Express_API.ModelsInfo;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Controllers
{
    [Route("api/percentbyage")]
    public class PercentByAgeController : Controller
    {
        private PercentByAgeService percentbyageService;
        public PercentByAgeController(PercentByAgeService _percentbyageService)
        {
            percentbyageService = _percentbyageService;
        }

        [HttpGet("findallpercentbyage")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllPercentByage()
        {
            try
            {
                return Ok(percentbyageService.FindAllPercentByAge());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findpercentbyage/{idprice}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindPercentByAge(int idprice)
        {
            try
            {
                return Ok(percentbyageService.FindPercentByAge(idprice));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("updatepercentbyage")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult UpdatePricePerKm([FromBody] PercentByAgeInfo percentbyageInfo)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    return Ok(percentbyageService.UpdatePercentByAge(percentbyageInfo));
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

    }
}
