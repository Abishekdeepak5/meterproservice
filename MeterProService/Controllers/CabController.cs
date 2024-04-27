using MeterProService.Components;
using MeterProService.Data;
using MeterProService.DTO;
using MeterProService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeterProService.Controllers
{
    //    /api/CabController
    [Route("api/[controller]")]
    [ApiController]
    public class CabController : ControllerBase
    {

        private readonly MeterproDbContext _meterproDbContext;
        private readonly CabUpdates _cabService;
        public CabController(MeterproDbContext _meterproDbContext, CabUpdates updateService)
        {

           // _meterproDbContext = _meterproDbContext ?? throw new ArgumentNullException(nameof(_meterproDbContext));
           // _cabService = _cabService ?? throw new ArgumentNullException(nameof(_cabService));

            this._meterproDbContext = _meterproDbContext;
            this._cabService = updateService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("showCab")]
        //public List<Cab> GetAvailableCabs()
         public  ActionResult GetCab()
        {
                //List<Cab> availableCabs = _meterproDbContext.Cab.Where(cab => (bool)cab.IsAvailable).ToList();
                var availableCab = _meterproDbContext.Cab.FirstOrDefault(cab => (bool)cab.IsAvailable);
                //if (availableCab == null)
                //{
                  //  return NotFound();
                //}
                return Ok(availableCab);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("startCab")]
        [Authorize]
        public ActionResult startUpdate([FromBody] CabDto cab)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userIdClaims = User.FindFirst("id");
                var id = userIdClaims?.Value;
                User_detail userDetail = _meterproDbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);
           
                var existingProduct = _meterproDbContext.Cab.FirstOrDefault(c =>c.carNumber == cab.carNumber && c.id==cab.cabId);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.IsAvailable = false;
                _meterproDbContext.SaveChanges();
                // Insert a record into the "trip" table
                Trip newTrip = new Trip
                {
                    userId = userDetail.id,
                    cabId = existingProduct.id,
                };

                _meterproDbContext.Trip.Add(newTrip);
                _meterproDbContext.SaveChanges();

                return Ok();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("endCab/{tripId?}")]
        [Authorize]
        public ActionResult endUpdate([FromBody] EndTripDto cab, int? tripId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userIdClaims = User.FindFirst("id");
                var id = userIdClaims?.Value;
                User_detail userDetail = _meterproDbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);
                if (userDetail == null)
                {
                    return NotFound();
                }

                var existingProduct = _meterproDbContext.Cab.FirstOrDefault(c => c.id == cab.cabId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.IsAvailable = true;
                _meterproDbContext.SaveChanges();

                var existingProduct1 = _meterproDbContext.Trip.FirstOrDefault(trip =>trip.id==tripId && trip.cabId == cab.cabId && trip.userId == userDetail.id);
                if (existingProduct1 == null)
                {
                    return new NotFoundObjectResult("not found");
                }

                existingProduct1.endLocation = cab.endAddress;
                existingProduct1.price = cab.price;
                existingProduct1.endTime = cab.endTime;
                existingProduct1.miles = cab.miles;
                _meterproDbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("startAddress")]
        [Authorize]
        public ActionResult startAddressUpdate([FromBody] StartAddressDto cab)
        {
            var userIdClaims = User.FindFirst("id");
            var id = userIdClaims?.Value;
            User_detail userDetail = _meterproDbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return _cabService.StartAddress(userDetail,cab);   
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]

        [Route("makeAvailableCab")]



        public ActionResult cabAvailableUpdate([FromBody] CabDto cab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingProduct = _meterproDbContext.Cab.FirstOrDefault(c => c.id == cab.cabId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.IsAvailable = true;
                _meterproDbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();

        }


    }
}

