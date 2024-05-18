using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IinstitutionServices _service;

        public InstitutionController(IinstitutionServices service)
        {
            _service = service;
        }

        // GET: api/<InstitutionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> Get()
        {
            var instututions = await _service.GetAll();
            return Ok(instututions);
        }

        // GET api/<InstitutionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Institution>> Get(int id)
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
        public async Task<IActionResult>CreateInstitution(Institution newInstitution)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateInstitution(newInstitution);

                return Ok(newInstitution);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<InstitutionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Institution institution)
        {
            if (id != institution.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateInstitution(id, institution);
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
            await _service.DeleteInstitution(id);
            return NoContent();
        }
    }
}
