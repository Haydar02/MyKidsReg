using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;
using MyKidsReg.Services.CommunicationsServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly CommunicationService _communicationService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, CommunicationService communicationService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _communicationService = communicationService;
            _logger = logger;
        }


        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult <IEnumerable<User>>> Get()
        {
            return await _userService.GetAlle();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.createUserWithTemporaryPAssword(user.User_Name, user.Name,
                                         user.Last_name, user.Address, user.Zip_code,
                                         user.E_mail, user.Mobil_nr, user.Usertype);

            return CreatedAtAction(nameof(GetUserById), new { id = user.User_Id }, user);
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult >UpdateUser(int id, User user)
        {
            if(id == user.User_Id)
            {
                return BadRequest();
            }
          await _userService.UpdateUser(id, user);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
