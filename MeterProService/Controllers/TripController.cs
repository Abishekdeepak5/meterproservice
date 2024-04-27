using MeterProService.Data;
using MeterProService.DTO;
using MeterProService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeterProService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly MeterproDbContext _meterproDbContext;
        public TripController(MeterproDbContext _meterproDbContext)
        {
            this._meterproDbContext = _meterproDbContext;
        }


        //get method to retrive the list of trip using user id
        //if the given user id is not in the trip 
        // it will return the empty list
        //else return the list of trip






        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize]
        public ActionResult TripsByUserId()
        {
            var userIdClaims = User.FindFirst("id");
            var id = userIdClaims?.Value;

            var trips = _meterproDbContext.Trip
                .Where(user => user.userId.ToString() == id)
                .OrderByDescending(trip => trip.id)
                .Select(trip => new TripHistoryDto
                    {
                          id = trip.id,
                          price = trip.price,
                          userId = trip.userId,
                          cabId=trip.cabId,
                          startTime=trip.startTime,
                          endTime=trip.endTime,
                          startLocation=trip.startLocation,
                          endLocation=trip.endLocation,
                          miles=trip.miles,
                    })
                .ToList();
            //return Ok(_meterproDbContext.Trip.Where(user => user.userId.ToString() == id).OrderByDescending(trip => trip.id).ToList());
            return Ok(trips);

            


        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("allTrip")]
        public ActionResult GetAllTrips()
        {

            return Ok(_meterproDbContext.Trip.Where(trip => true));
        }
        //post : trip

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Post(int userId, [FromBody] Trip trip)
        {
                //check if the user with the give userid is present
                var user = _meterproDbContext.User_detail
                .FirstOrDefault(user=>user.id == userId);

            if (user == null)
                return NotFound($"user with {userId} is Not Found");

            trip.userId = userId;
            trip.user = user;

            _meterproDbContext.Trip.Add(trip);
            _meterproDbContext.SaveChanges();
            return Ok("trip added successfully");

        }
        //delete the trip

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public ActionResult removeTrip(int tripId)
        {
                var trip = _meterproDbContext.Trip.FirstOrDefault(trip => trip.id == tripId);
            if (trip == null)
                return NotFound($"trip with trip id {tripId}");
            _meterproDbContext.Remove(trip);
            _meterproDbContext.SaveChanges();
            return Ok("trip is deleted");
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public ActionResult Update(int id,[FromBody] LatLngDto trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingProduct = _meterproDbContext.Trip.Find(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.latitude = trip.latitude;
                existingProduct.longitude = trip.longitude;
                _meterproDbContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
            // _meterproDbContext.Trip.Update(trip);
            //_meterproDbContext.SaveChanges();
            //return NoContent();

        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet]
        //[Route("allData")]
        //public async IEnumerable<> GetAllDatas()
        //{

            //return Ok(_meterproDbContext.Trip.Where(trip => true));
        //}



    }
}

