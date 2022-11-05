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

        public async Task<IEnumerable<FactoryDevice>> GetAll()
        {
            return await _factoryDevicesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<FactoryDevice> Get(string id)
        {
            return await _factoryDevicesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}