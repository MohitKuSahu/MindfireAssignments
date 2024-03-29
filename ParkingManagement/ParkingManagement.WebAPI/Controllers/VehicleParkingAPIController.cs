using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.BL;
using ParkingManagement.DAL.Models;
using ParkingManagement.Models;
using ParkingManagement.Utils;

namespace ParkingManagement.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleParkingAPIController : ControllerBase
    {
        private readonly IBL _BAL;
        private readonly ILog _Log;

        public VehicleParkingAPIController(IBL bAL, ILog log)
        {
            _BAL = bAL;
            _Log = log;
        }

      
        [HttpGet]
        public async Task<ActionResult<List<VehicleParkingModel>>> GetVehicleParking()
        {
            try
            {
                var data = await _BAL.ListVehicleParkingAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return NotFound();
            }
        }

        [Authorize(Roles = "Booking Counter Agent")]
        [HttpGet("{parkingSpaceId}")]
        public async Task<ActionResult<List<VehicleParkingModel>>> GetVehicleParkingById(int parkingSpaceId)
        {
            try
            {
                var data = await _BAL.ListVehicleParkingByIdAsync(parkingSpaceId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return NotFound();
            }
        }


        [Authorize(Roles = "Booking Counter Agent")]
        [HttpPut]
        public async Task<ActionResult<VehicleParkingModel>> UpdateVehicle(VehicleParkingModel model)
        {
            try
            {
                bool updateResult = await _BAL.UpdateVehicleAsync(model);
                if (updateResult)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, "A vehicle with the same number already Booked");
                }
            }
            catch (Exception ex) { 
            
                _Log.AddException(ex);  
                return BadRequest(ex);  
            }

        }

        [Authorize(Roles = "Booking Counter Agent")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<VehicleParkingModel>> DeleteVehicle(int Id)
        {
            try
            {
                var data = await _BAL.DeleteVehicleParkingAsync(Id);
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
