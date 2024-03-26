using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.BL;
using ParkingManagement.Models;
using ParkingManagement.Utils;

namespace ParkingManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IBL _userBAL;
        private readonly ILog _Log;

        public UserAPIController(IConfiguration config, IBL userBAL, ILog Log)
        {
            _config = config;
            _userBAL = userBAL;
            _Log = Log;
        }
        [HttpPost]
        public async Task<ActionResult> InsertUser(UserModel user)
        {

            bool flag = await _userBAL.InsertUser(user);
            if (flag)
            {
                return Ok(flag);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{email}")]
        public async Task<ActionResult> CheckEmail(string email)
        {
            bool success = await _userBAL.CheckIfEmailAlreadyExists(email);
            if (success)
            {
                return Ok(new { success = true });
            }
            else
            {
                return Ok(new { success = false });
            }
        }
    }
}
