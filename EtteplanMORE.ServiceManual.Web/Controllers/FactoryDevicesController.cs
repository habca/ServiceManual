using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactoryDevicesController : Controller
    {
        private readonly IFactoryDeviceService _factoryDeviceService;

        public FactoryDevicesController(IFactoryDeviceService factoryDeviceService)
        {
            _factoryDeviceService = factoryDeviceService;
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _factoryDeviceService.GetAll())
                .Select(fd =>
                    new FactoryDeviceDto
                    {
                        Id = fd.Id,
                        Name = fd.Name,
                        Year = fd.Year,
                        Type = fd.Type
                    }
                ));
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/1
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var fd = await _factoryDeviceService.Get(id);
            if (fd == null)
            {
                return NotFound();
            }

            return Ok(new FactoryDeviceDto
            {
                Id = fd.Id,
                Name = fd.Name,
                Year = fd.Year,
                Type = fd.Type
            });
        }

        /// <summary>
        ///     HTTP POST: api/factorydevices/
        /// </summary> 
        [HttpPost]
        public async Task<ActionResult> Post(FactoryDeviceDto dto)
        {
            var fd = await _factoryDeviceService.Get(dto.Id);
            if (fd != null)
            {
                return BadRequest();
            }

            fd = await _factoryDeviceService.Post(new FactoryDevice
            {
                Name = dto.Name,
                Year = dto.Year,
                Type = dto.Type
            });

            return CreatedAtAction(
                nameof(Get),
                new { id = fd.Id },
                new FactoryDeviceDto
                {
                    Id = fd.Id,
                    Name = fd.Name,
                    Year = fd.Year,
                    Type = fd.Type
                }
            );
        }

        /// <summary>
        ///     HTTP PUT: api/factorydevices/
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(FactoryDeviceDto dto)
        {
            var fd = await _factoryDeviceService.Get(dto.Id);
            if (fd == null)
            {
                return NotFound();
            }

            fd.Name = dto.Name;
            fd.Type = dto.Type;
            fd.Year = dto.Year;

            await _factoryDeviceService.Put(fd);
            return NoContent();
        }

        /// <summary>
        ///     HTTP DELETE: api/factorydevices/
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(FactoryDeviceDto dto)
        {
            var fd = await _factoryDeviceService.Get(dto.Id);
            if (fd == null)
            {
                return NotFound();
            }

            await _factoryDeviceService.Delete(fd);
            return NoContent();
        }
    }
}