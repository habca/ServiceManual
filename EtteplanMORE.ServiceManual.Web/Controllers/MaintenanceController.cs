using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenanceController : GenericCrudController<Maintenance>
    {
        public MaintenanceController(IFactoryDeviceService<Maintenance> factoryDeviceService) : base(factoryDeviceService)
        {
        }
    }
}