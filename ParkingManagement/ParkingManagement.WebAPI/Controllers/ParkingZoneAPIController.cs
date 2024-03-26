using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.BL;
using ParkingManagement.Models;
using ParkingManagement.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ParkingManagement.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingZoneAPIController : ControllerBase
    {
        private readonly IBL _BAL;
        private readonly ILog _Log;

        public ParkingZoneAPIController(IBL BAL, ILog Log)
        {
            _BAL = BAL;
            _Log = Log;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ParkingZoneModel>>> GetParkingZone()
        {
            try
            {
                var data = await _BAL.ListParkingZoneAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ParkingZoneModel>> AddParkingZone(ParkingZoneModel model)
        {
            try
            {
                int parkingZoneId=await _BAL.AddParkingZoneAsync(model);
                model.ParkingZoneId = parkingZoneId;
                return Ok(model);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ParkingZoneModel>> DeleteParkingZone(int Id)
        {
            try
            {
                var data = await _BAL.DeleteParkingZoneAsync(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return BadRequest(ex);
            }
        }
    }
}
