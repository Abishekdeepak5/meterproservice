using MeterProService.Data;
using MeterProService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeterProService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly MeterproDbContext _dbContext;


        public UserDetailController(MeterproDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        //withoud id get method

        [Authorize]
        public ActionResult Get()
        {
            var User_detailList = _dbContext.User_detail.ToList();

            return Ok(User_detailList);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        [Authorize]
        public ActionResult GetUserDetails()
        {
            var userIdClaims = User.FindFirst("id");
            var id = userIdClaims?.Value;
            User_detail userDetai = _dbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);
            return Ok(userDetai);


        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public ActionResult Create([FromBody] User_detail Userdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.User_detail.Add(Userdetail);
            _dbContext.SaveChanges();
            return Ok();

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public ActionResult Update([FromBody] User_detail Userdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.User_detail.Update(Userdetail);
            _dbContext.SaveChanges();
            return NoContent();
        }

       /* [HttpGet("Rec/{id?}")]
        //[HttpGet("Rec")]
        public async Task<IActionResult> Get(int? id)

        {

            return Ok(id);
        }*/

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var resultRow = _dbContext.User_detail.FirstOrDefault(x => x.id == id);
            if (resultRow == null)
            {
                return NotFound();
            }
            _dbContext.User_detail.Remove(resultRow);
            _dbContext.SaveChanges();
            return Ok();

        }
        }
}
