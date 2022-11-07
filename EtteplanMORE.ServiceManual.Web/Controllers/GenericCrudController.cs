using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    public class GenericCrudController<T, S> : ControllerBase where T : IEntity<S>
    {
        protected readonly IFactoryDeviceService<T, S> _service;

        public GenericCrudController(IFactoryDeviceService<T, S> service)
        {
            _service = service;
        }

        /// <summary>
        /// Every equipment in the factory that are subject to maintenance.
        /// </summary>
        /// <remarks>The result may not have any record at all.</remarks>
        /// <returns>All entries from the database in an array.</returns>
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        /// <summary>
        /// A single equipment that matches the unique id.
        /// </summary>
        /// <remarks>A situation where the id is empty should not be possible.</remarks>
        /// <param name="id">Identifies the factory device.</param>
        /// <returns>A single entry from the database when the id matches.</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(S id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var obj = await _service.Get(id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        /// <summary>
        /// To record maintenance task of a factory equipment.
        /// </summary>
        /// <remarks>The id must be empty, as it will be assigned by the database.</remarks>
        /// <param name="obj">The maintenance task to be recorded.</param>
        /// <returns>Record of a newly added maintenance task.</returns>
        [HttpPost]
        public virtual async Task<ActionResult> Post(T obj)
        {
            if (obj == null || obj.Id != null)
            {
                return BadRequest();
            }

            obj = await _service.Post(obj);
            return CreatedAtAction(
                nameof(Get), new { id = obj.Id }, obj);
        }

        /// <summary>
        /// Update a previously added maintenance task.
        /// </summary>
        /// <remarks>The id should not be empty.</remarks>
        /// <param name="obj">The maintenance task to be updated.</param>
        /// <returns>Succeeds silently and returns nothing.</returns>
        [HttpPut]
        public virtual async Task<IActionResult> Put(T obj)
        {
            if (obj == null || obj.Id == null)
            {
                return BadRequest();
            }

            var fd = await _service.Get(obj.Id);
            if (fd == null)
            {
                return NotFound();
            }

            await _service.Put(obj);
            return NoContent();
        }

        /// <summary>
        /// Remove a maintenance task that will never be carried out.
        /// </summary>
        /// <remarks>
        /// Removing an equipment does not remove the associated maintenance tasks.
        /// </remarks>
        /// <param name="obj">The maintenance task to be removed.</param>
        /// <returns>Succeeds silently and returns nothing.</returns>
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(T obj)
        {
            if (obj == null || obj.Id == null)
            {
                return BadRequest();
            }

            var found = await _service.Get(obj.Id);
            if (found == null)
            {
                return NotFound();
            }

            await _service.Delete(obj);
            return NoContent();
        }
    }
}