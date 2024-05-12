using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageServices _service;

        public MessageController(IMessageServices service)
        {
            _service = service;
        }

        // GET: api/<MessageController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetByID(int id)
        {
            var item = await _service.GetByID(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/<MessageController>
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message newMessage)
        {
            try
            {
                if (newMessage == null)
                {
                    return BadRequest("Message object is null");
                }

                await _service.CreateMessage(newMessage);
                return CreatedAtAction(nameof(GetByID), new { id = newMessage.Message_id }, newMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        //// PUT api/<MessageController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var foundMessage = await _service.GetByID(id);
                if (foundMessage == null)
                {
                    return NotFound();
                }
                await _service.DeleteMessage(id);
                return Ok(foundMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"Det er ingen indbakke : {ex.Message}");
            }
        }
    }
}
