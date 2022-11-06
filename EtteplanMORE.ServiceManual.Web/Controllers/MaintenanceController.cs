using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenanceController : GenericCrudController<Maintenance, string>
    {
        public MaintenanceController(IFactoryDeviceService<Maintenance, string> factoryDeviceService) : base(factoryDeviceService)
        {
        }

        public override async Task<IActionResult> Get()
        {
            IEnumerable<Maintenance> array = await _service.GetAll();
            array = array.OrderBy(c => c.DateTime);
            array = array.OrderBy(c => c.Criticality);
            return Ok(array);
        }

        [HttpGet("filter/{fdId}")]
        public async Task<IActionResult> Filter(string fdId)
        {
            if (fdId == null)
            {
                return BadRequest();
            }

            IEnumerable<Maintenance> array = await _service.GetAll();
            array = array.Where(c => c.FactoryDevice == fdId);
            return Ok(array);
        }
    }
}