using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarrierTrackingAPI.Controllers;
using CarrierTrackingAPI.Interfaces;
using CarrierTrackingAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace CarrierTrackingAPI.Services
{
    public class MapHubDispatcher : IMapHubDispatcher
    {
        private readonly IHubContext<MapHub> _hubContext;

        public MapHubDispatcher(IHubContext<MapHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task UpdateMapLocation(LocationModel location)
        {
            await this._hubContext.Clients.All.SendAsync("UpdateCourierLocation", location);
        }
    }
}
