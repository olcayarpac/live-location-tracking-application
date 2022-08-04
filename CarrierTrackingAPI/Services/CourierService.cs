using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarrierTrackingAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarrierTrackingAPI.Services
{
    public class CourierService
    {
        private readonly IMongoCollection<LocationModel> _courierLocationsCollection;

        public CourierService(IOptions<Configuration> courierDBSettings)
        {
            var mongoClient = new MongoClient(courierDBSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(courierDBSettings.Value.DatabaseName);
            _courierLocationsCollection = mongoDb.GetCollection<LocationModel>(
                courierDBSettings.Value.LocationsCollectionName);
        }

        public async Task<List<LocationModel>> GetAsync() =>
            await _courierLocationsCollection.Find(_ => true).ToListAsync();

        public async Task UpdateAsync(LocationModel updatedLocation) =>
            await _courierLocationsCollection.ReplaceOneAsync(x => x.Id == updatedLocation.Id, updatedLocation);
            

    }
}
