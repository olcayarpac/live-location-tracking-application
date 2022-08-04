using CarrierTrackingAPI.Interfaces;
using CarrierTrackingAPI.Models;
using CarrierTrackingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarrierTrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly CourierService _courierService;
        private readonly IMapHubDispatcher _dispatcher;
        public MapController(CourierService courierService, IMapHubDispatcher dispatcher)
        {
            _courierService = courierService;
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [Route("getAllCourierLocations")]
        public async Task<List<LocationModel>> GetAllCourierLocations()
        {
            return await _courierService.GetAsync();
        }

        [HttpPost("UpdateLocation")]
        public async Task UpdateLocation([FromBody] LocationModel location)
        {
            await _courierService.UpdateAsync(location);
            await this._dispatcher.UpdateMapLocation(location);
        }

    }
}
