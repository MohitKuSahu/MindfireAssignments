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

        private readonly IBL _BAL;
        private readonly ILog _Log;

        public UserAPIController(IBL userBAL, ILog Log)
        {
            _BAL = userBAL;
            _Log = Log;
        }
        [HttpPost]
        public async Task<ActionResult> InsertUser(UserModel user)
        {
            try
            {
                bool flag = await _BAL.InsertUser(user);
                if (flag)
                {
                    return Ok(flag);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return BadRequest(ex);
            }
            
        }

        [HttpPost("{email}")]
        public async Task<ActionResult> CheckEmail(string email)
        {
            try
            {
                bool success = await _BAL.CheckIfEmailAlreadyExists(email);
                if (success)
                {
                    return Ok(new { success = true });
                }
                else
                {
                    return Ok(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return BadRequest(ex);
            }   
             
        }
    }
}
