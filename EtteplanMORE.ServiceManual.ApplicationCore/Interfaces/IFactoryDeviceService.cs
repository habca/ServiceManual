using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IFactoryDeviceService
    {
        Task<IEnumerable<FactoryDevice>> GetAll();

        Task<FactoryDevice> Get(string? id);

        Task<FactoryDevice> Post(FactoryDevice factoryDevice);

        Task Put(FactoryDevice factoryDevice);

        Task Delete(FactoryDevice factoryDevice);
    }
}