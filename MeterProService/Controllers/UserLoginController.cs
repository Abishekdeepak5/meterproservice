using MeterProService.Data;
using MeterProService.DTO;
using MeterProService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeterProService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private MeterproDbContext _meterProDbContext;
        public UserLoginController(MeterproDbContext meterproDbContext)
        {
            _meterProDbContext = meterproDbContext;
        }


        [Route("/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult userLogin(UserLogin userLogin)
        {
            // chech is the user with the give password is found in database or not
            // if present return the UserDetail object which contain token
            // store the token in the database
            // if not in the database return user not found
            var currentUser = authenticateuser(userLogin);
            if (currentUser != null)
            {
                
                var result = new UserDetail();
                result.Token = generatetoken(currentUser);
                result.FirstName = currentUser.first_name;
                result.LastName = currentUser.last_name;
                return Ok(result);
            }
            return NotFound("user not found");
            
        }

        [NonAction]
        public User_detail? authenticateuser(UserLogin userLogin)
        {
            var userName = userLogin.userName;
            var password = userLogin.password;
            var currentUser = _meterProDbContext.User_detail.FirstOrDefault(user => user.user_name.ToLower() == userName.ToLower() && user.password.ToLower() == password.ToLower());


            return currentUser;

            

        }
        [NonAction]
        public string generatetoken(User_detail currentUser)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("apwmdlliendaddnetknz=3mlkd652341");
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", currentUser.user_name),
                new Claim("id",currentUser.id.ToString())
                }
                ),
                Audience = "user",
                Issuer = "user",
                Expires = DateTime.UtcNow.AddDays(7),
               // Expires = DateTime.UtcNow.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            return tokenhandler.WriteToken(token);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        [Route("validate")]
        [AllowAnonymous]
        public ActionResult ValidateToken([FromBody]TokenDto userToken)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes("apwmdlliendaddnetknz=3mlkd652341");
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "user",
                    ValidAudience = "user",
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero   
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(userToken.token, validationParameters, out validatedToken);

                // Check if the token is not expired
                var expirationDate = validatedToken.ValidTo;
                var currentDate = DateTime.UtcNow;
                var isTokenValid = expirationDate > DateTime.UtcNow;
                var daysBeforeExpiration = 3;
                var thresholdDate = expirationDate.AddDays(-daysBeforeExpiration);
               // var thresholdDate = expirationDate.AddSeconds(-210);           
                if (currentDate >= thresholdDate)
                {
                    var newToken = IssueNewToken();
                    if (newToken == null)
                    {
                        return NotFound();
                    }
                    return Ok(newToken);
                }
                else
                {
                return NoContent(); ;
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Token validation failed: {ex.Message}");
            }
        }
        private string IssueNewToken()
        {
            var userIdClaims = User.FindFirst("id");
            var id = userIdClaims?.Value;
            User_detail userDetail = _meterProDbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);
            var userLogin = new UserLogin
            {
                userName = userDetail.user_name,
                password = userDetail.password
            };
            var currentUser = authenticateuser(userLogin);
            if (currentUser != null)
            {

                String token=generatetoken(currentUser);
                return token;
            }
            return null;
        }
    }
}
                //var result = new UserDetail();
                //result.Token = generatetoken(currentUser);
                //result.FirstName = currentUser.first_name;
                //result.LastName = currentUser.last_name;
