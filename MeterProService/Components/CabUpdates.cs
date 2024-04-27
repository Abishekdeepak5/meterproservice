using MeterProService.Data;
using MeterProService.DTO;
using MeterProService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeterProService.Components
{
    public class CabUpdates
    {
        private readonly MeterproDbContext _dbContext;
        public CabUpdates(MeterproDbContext _meterproDbContext)
        {
            this._dbContext = _meterproDbContext;
        }

        public ActionResult StartAddress(User_detail user, StartAddressDto cab)
        {
           
            try
            {
                var existingProduct = _dbContext.Cab.FirstOrDefault(c =>c.id == cab.cabId);
                if (existingProduct == null)
                {
                    return new NotFoundObjectResult("not found");
                }
                existingProduct.IsAvailable = false;
                Trip newTrip = new Trip
                {
                    userId = user.id,
                    cabId = existingProduct.id,
                    startLocation= cab.startAddress,
                    startTime= cab.startTime
                };
                _dbContext.Trip.Add(newTrip);
                _dbContext.SaveChanges();
                return new OkObjectResult(newTrip.id);
            }
            catch
            {
                return new NotFoundObjectResult("not found");
            }
        }
    }
}
