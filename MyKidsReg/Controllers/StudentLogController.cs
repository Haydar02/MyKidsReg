using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLogController : ControllerBase
    {
        private readonly IStudentLogServices _service;

        public StudentLogController(IStudentLogServices service)
        {
            _service = service;
        }

        // GET: api/<StudentLogController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentLog>>> Get()
        {
            var studentLogs = await _service.GetAll();
            return Ok(studentLogs);
        }

        // GET api/<StudentLogController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentLog>> Get(int id)
        {
            var studentLog = await _service.GetById(id);
            if (studentLog == null)
            {
                return NotFound();
            }
            return Ok(studentLog);
        }

        // POST api/<StudentLogController>
        [HttpPost]
        public async Task<IActionResult> CreateStudentLog(StudentLog studentLog)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateStudentLog(studentLog);

                return Ok(studentLog);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<StudentLogController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentLog studentLog)
        {
            if (id != studentLog.id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateStudentLog(id, studentLog);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        // DELETE api/<StudentLogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteStudentLog(id);
            return NoContent();
        }
    }
}
