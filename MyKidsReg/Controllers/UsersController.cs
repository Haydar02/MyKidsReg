using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _userService.createUserWithTemporaryPAssword(user.User_Name, user.Name,
                                     user.Last_name, user.Address, user.Zip_code,
                                     user.E_mail, user.Mobil_nr, user.Usertype);

            // Her kan du kalde SendTemoraryPassword fra CommunicationService for at sende en velkomst-e-mail
            try
            {
                _communicationService.SendEmail(user.E_mail, "MidlertidigAdgangskode", "email");
            }
            catch (Exception ex)
            {
                // Håndter eventuelle fejl, f.eks. logning af dem
                _logger.LogError($"Fejl under afsendelse af velkomst-e-mail til bruger {user.E_mail}: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.User_Id }, user);
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if(id == user.User_Id)
            {
                return BadRequest();
            }
           _userService.UpdateUser(id, user);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
