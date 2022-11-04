using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using MongoDB.Driver;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : IFactoryDeviceService
    {
        private readonly IMongoCollection<FactoryDevice> _factoryDevicesCollection;

        public FactoryDeviceService()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");

            var mongoDatabase = mongoClient.GetDatabase("ServiceManual");

            _factoryDevicesCollection = mongoDatabase.GetCollection<FactoryDevice>("FactoryDevices");
        }

        /// <summary>
        ///     Remove this. Temporary device storage before proper data storage is implemented.
        /// </summary>
        private static readonly ImmutableList<FactoryDevice> TemporaryDevices = new List<FactoryDevice>
        {
            new FactoryDevice
            {
                Id = "1",
                Name = "Device X",
                Year = 2001,
                Type = "Type 10"
            },
            new FactoryDevice
            {
                Id = "2",
                Name = "Device Y",
                Year = 2012,
                Type = "Type 3"
            },
            new FactoryDevice
            {
                Id = "3",
                Name = "Device Z",
                Year = 1985,
                Type = "Type 1"
            }
        }.ToImmutableList();

        public async Task<IEnumerable<FactoryDevice>> GetAll()
        {
            return await Task.FromResult(TemporaryDevices);
        }

        public async Task<FactoryDevice> Get(string id)
        {
            return await Task.FromResult(TemporaryDevices.FirstOrDefault(c => c.Id == id));
        }

        public async Task<List<FactoryDevice>> GetAsync() =>
            await _factoryDevicesCollection.Find(_ => true).ToListAsync();
    }
}