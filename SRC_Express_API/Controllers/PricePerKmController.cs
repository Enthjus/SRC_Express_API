using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRC_Express_API.ModelsInfo;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Controllers
{
    [Route("api/priceperkm")]
    public class PricePerKmController : Controller
    {

        private PricePerKmService priceperkmService;
        public PricePerKmController(PricePerKmService _priceperkmService)
        {
            priceperkmService = _priceperkmService;
        }

        [HttpGet("findallpriceperkm")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllPricePerKm()
        {
            try
            {
                return Ok(priceperkmService.FindAllPricePerKm());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("findpriceperkmbyid/{idprice}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllPricePerKm(int idprice)
        {
            try
            {
                return Ok(priceperkmService.FindPricePerKm(idprice));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("updatepriceperkm")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult UpdatePricePerKm([FromBody] PricePerKmInfo priceperkmInfo)
        {
            Debug.WriteLine(priceperkmInfo.Id);
            try
            {
                if (ModelState.IsValid)
                {

                    return Ok(priceperkmService.UpdatePricePerKm(priceperkmInfo));
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
