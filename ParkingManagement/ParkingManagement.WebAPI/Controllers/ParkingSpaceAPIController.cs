using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.BL;
using ParkingManagement.Utils;
using ParkingManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace ParkingManagement.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpaceAPIController : ControllerBase
    {
        private readonly IBL _BAL;
        private readonly ILog _Log;

        public ParkingSpaceAPIController(IBL BAL, ILog Log)
        {
            _BAL = BAL;
            _Log = Log;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ParkingSpaceModel>>> GetParkingSpace()
        {
            try
            {
                var data = await _BAL.ListParkingSpaceAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return NotFound();
            }

        }

        [Authorize]
        [HttpGet("{ParkingZoneId}")]
        public async Task<ActionResult<List<ParkingSpaceModel>>> ListParkingSpaceById(int ParkingZoneId)
        {
            try
            {
                var data = await _BAL.ListParkingSpaceByIdAsync(ParkingZoneId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return NotFound();
            }
        }

        [Authorize(Roles = "Booking Counter Agent")]
        [HttpPost]
        public async Task<ActionResult<ParkingSpaceModel>> AddParkingSpace(ParkingSpaceModel model)
        {
            try
            {
                bool success = await _BAL.AddParkingSpaceAsync(model);
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


        [Authorize(Roles = "Booking Counter Agent")]
        [HttpDelete("{title}")]
        public async Task<ActionResult<ParkingSpaceModel>> DeleteParkingSpace(string title)
        {
            try
            {
                var success = await _BAL.DeleteParkingSpaceAsync(title);
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
