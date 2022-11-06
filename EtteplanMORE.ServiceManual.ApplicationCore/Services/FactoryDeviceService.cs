using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using MongoDB.Driver;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : IFactoryDeviceService<FactoryDevice>
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

        public async Task<FactoryDevice> Get(string? id)
        {
            return await _factoryDevicesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<FactoryDevice> Post(FactoryDevice factoryDevice)
        {
            await _factoryDevicesCollection.InsertOneAsync(factoryDevice);

            // Return the newly added entity, because the primary key is automatically generated.
            // Otherwise the user should search for the most recent record, which is not thread-safe.
            return await Get(factoryDevice.Id);
        }

        public async Task Put(FactoryDevice factoryDevice)
        {
            await _factoryDevicesCollection.ReplaceOneAsync(c => c.Id == factoryDevice.Id, factoryDevice);
        }

        public async Task Delete(FactoryDevice factoryDevice)
        {
            await _factoryDevicesCollection.DeleteOneAsync(c => c.Id == factoryDevice.Id);
        }
    }
}