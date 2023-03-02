using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRC_Express_API.Models;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SRC_Express_API.Controllers
{
    [Route("api/tickets")]
    public class TicketsController : Controller
    {
        private TicketService ticketService;
        public TicketsController(TicketService _ticketService)
        {

            ticketService = _ticketService;
        }

        [HttpGet("findallticket")]
        //[Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindAllTicket()
        {
            try
            {

                return Ok(ticketService.FindAllTicket());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("findlistseats/{idactivetrip}/{dayStart}/{monthStart}/{yearStart}")]
        //[Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult FindListSeats(int idactivetrip,int dayStart,int monthStart,int yearStart)
        {
            try
            {

                return Ok(ticketService.GetListSeats(idactivetrip,dayStart,monthStart,yearStart));
        }
            catch
            {
                return BadRequest();
    }
}


        [HttpGet("gettotaltam/{idactiveTrip}/{age}")]
        //[Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult GetTotalTam(int idactiveTrip, int age)
        {
            try
            {
                var total = ticketService.GetTotalTam(idactiveTrip, age);
                if (total >= 0)
                {
                    return Ok(total);
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

        [HttpPost("createticket")]
        //[Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult CreateTicket([FromBody] Ticket tk)
        {
            try
            {
                var newtk = ticketService.CreateTicket(tk);
                if (newtk == null)
                {
                    return BadRequest();
                }
                else
                {
                    var email = newtk.IdAccCustomer.Email;
                    var content = "Your Booking Code: "+newtk.Id+"<br/>";
                    content += "Name: " + newtk.IdAccCustomer.FullName + "<br/>";
                    content+="From Address: "+newtk.IdactiveTrip.Trip.FromAddress+"<br/>";
                    content += "To Address: " + newtk.IdactiveTrip.Trip.ToAddress + "<br/>";
                    content += "Start Date: " + newtk.StartDate.ToString().Replace(" 12:00:00 AM", "") + "<br/>";
                    content += "Start Time: " + newtk.IdactiveTrip.TimeStart.TimeStart.ToString().Replace("1/1/1900 ", "") + "<br/>";
                    content += "Your Car: " + newtk.IdactiveTrip.Car.Id + "<br/>";
                    content += "Type Car: " + newtk.IdactiveTrip.Car.TypeName + "<br/>";
                    content += "Seat: " + newtk.Seats + "<br/>";
                    if ( ticketService.SendMail(email, "Ticket Info:", content, true))
                    {
                        return Ok(newtk);
                    }
                    else
                    {
                        return BadRequest("Send Maill Errr");
                    }
                   
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("refundticket/{idticket}")]
        //[Authorize(Roles = "SA")]
        [Produces("application/json")]
        public IActionResult RefundTicket(int idticket)
        {
            try
            {    
                    return Ok(ticketService.Refund(idticket));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
