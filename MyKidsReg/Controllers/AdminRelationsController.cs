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
    public class AdminRelationsController : ControllerBase
    {
        private readonly IAdminRelationServices _service;

        public AdminRelationsController(IAdminRelationServices service)
        {
            _service = service;
        }

        // GET: api/<AdminRelationsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminRelation>>> Get()
        {
            var adminRelations = await _service.GetAll();
            return Ok(adminRelations);
        }

        // GET api/<AdminRelationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminRelation>> Get(int id)
        {
            var adminRelation = await _service.GetById(id);
            if (adminRelation == null)
            {
                return NotFound();
            }
            return Ok(adminRelation);
        }

        // POST api/<AdminRelationsController>
        [HttpPost]
        public async Task<IActionResult> CreateAdminRelation(AdminRelation newAdminRelation)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateAdminRelations(newAdminRelation);

                return Ok(newAdminRelation);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<AdminRelationsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AdminRelation adminRelation)
        {

            if (id != adminRelation.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateAdminRelations(id, adminRelation);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }

        // DELETE api/<AdminRelationsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundItem = await _service.GetById(id);
            if (foundItem == null)
            { return NotFound(); }
            await _service.DeleteAdminRelations(id);
            return Ok(foundItem);
        }
    }
}
