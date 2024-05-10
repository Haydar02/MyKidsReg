﻿using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IinstututionServices _service;

        public InstitutionController(IinstututionServices service)
        {
            _service = service;
        }

        // GET: api/<InstitutionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instutution>>> Get()
        {
            var instututions = await _service.GetAll();
            return Ok(instututions);
        }

        // GET api/<InstitutionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instutution>> Get(int id)
        {
            var instutution = await _service.GetById(id);
            if (instutution == null)
            {
                return NotFound();
            }
            return Ok(instutution);
        }

        // POST api/<InstitutionController>
        [HttpPost]
        public async Task<IActionResult>CreateInstutution(Instutution newInstutution)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateInstutution(newInstutution);

                return Ok(newInstutution);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<InstitutionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Instutution instutution)
        {
            if (id != instutution.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateInstutution(id, instutution);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        // DELETE api/<InstitutionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteInstutution(id);
            return NoContent();
        }
    }
}
