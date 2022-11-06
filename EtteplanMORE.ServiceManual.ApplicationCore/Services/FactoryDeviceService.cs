using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : GenericDatabaseService<FactoryDevice, string>
    {
        public FactoryDeviceService() : base(
            "mongodb://localhost:27017",
            "ServiceManual",
            "FactoryDevices"
        )
        {
        }
    }
}