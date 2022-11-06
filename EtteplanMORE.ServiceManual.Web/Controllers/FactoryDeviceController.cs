using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [ApiController]
    [Route("api/factorydevices")]
    public class FactoryDeviceController : GenericCrudController<FactoryDevice>
    {
        public FactoryDeviceController(IFactoryDeviceService<FactoryDevice> factoryDeviceService) : base(factoryDeviceService)
        {
        }
    }
}