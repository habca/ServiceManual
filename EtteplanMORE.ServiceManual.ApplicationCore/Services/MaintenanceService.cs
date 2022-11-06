using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class MaintenanceService : GenericDatabaseService<Maintenance, string>
    {
        public MaintenanceService() : base(
            "mongodb://localhost:27017",
            "ServiceManual",
            "Maintenances"
        )
        {
        }
    }
}