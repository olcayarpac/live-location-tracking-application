using CarrierTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarrierTrackingAPI.Interfaces
{
    public interface IMapHubDispatcher
    {
        Task UpdateMapLocation(LocationModel location);
    }
}
