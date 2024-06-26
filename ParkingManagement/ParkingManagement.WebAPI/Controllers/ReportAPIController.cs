﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Models;
using ParkingManagement.BL;
using ParkingManagement.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ParkingManagement.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportAPIController : ControllerBase
    {

        private readonly IBL _BAL;
        private readonly ILog _Log;

        public ReportAPIController(IBL bAL, ILog log)
        {
            _BAL = bAL;
            _Log = log;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReportModel>>> GetParkingSpaceLists()
        {
            try
            {
                var data = await _BAL.GetParkingZoneandGetParkingSpaceAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return BadRequest(ex);
            }

        }

        [Authorize(Roles = "Booking Counter Agent")]
        [HttpGet("{startDate}/{endDate}")]
        public async Task<ActionResult<List<ReportModel>>> GetReportAsync(string startDate, string endDate)
        {
            try
            {

                if (!DateOnly.TryParse(startDate, out var parsedStartDate))
                {
                    return BadRequest($"Invalid start date format: {startDate}");
                }

                if (!DateOnly.TryParse(endDate, out var parsedEndDate))
                {
                    return BadRequest($"Invalid end date format: {endDate}");
                }

                var reportData = await _BAL.GetParkingReportAsync(parsedStartDate, parsedEndDate);
                return Ok(reportData);
            }
            catch (Exception ex)
            {
                _Log.AddException(ex);
                return BadRequest(ex);
            }
        
        }
    }
}
