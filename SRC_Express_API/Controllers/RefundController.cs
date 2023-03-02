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
    [Route("api/refund")]
    public class RefundController : Controller
    {
        private RefundService refundService;
        public RefundController(RefundService _refundService)
        {
            refundService = _refundService;
        }

        [HttpGet("findallrefund")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllPercentByage()
        {
            try
            {
                return Ok(refundService.FindAllRefund());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findrefund/{idprice}")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindPercentByAge(int idprice)
        {
            try
            {
                return Ok(refundService.FindRefund(idprice));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updaterefund")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult UpdatePricePerKm([FromBody] RefundInfo refundInfo)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    return Ok(refundService.UpdateRefund(refundInfo));
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

        [HttpGet("findallrequestrefund")]
        [Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllRequestRefund()
        {
            try
            {
                return Ok(refundService.FindAllRequestRefund());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updatestatusrequestrefund/{idRequestRefund}")]
        //[Authorize(Roles = "SA")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult UpdateStatusRequestRefund(int idRequestRefund)
        {

            try
            {

                    return Ok(refundService.UpdateStatusRequestRefund(idRequestRefund));

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
