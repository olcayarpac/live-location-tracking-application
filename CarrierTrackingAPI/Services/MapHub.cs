using CarrierTrackingAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarrierTrackingAPI.Controllers
{
    public class MapHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("GetCourierLocations");
        }

        public async Task UpdateCourierLocations(LocationModel courierLocation)
        {
            await Clients.All.SendAsync("UpdateCourierLocation", courierLocation);
        }
    }
}
