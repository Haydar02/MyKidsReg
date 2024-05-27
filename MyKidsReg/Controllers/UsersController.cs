using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;
using MyKidsReg.Services.CommunicationsServices;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    //URL: api/MykidsReg
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginRequest)
        {
            try
            {
                // Log besked før login-forsøget
                _logger.LogInformation($"Attempting to login with username: {loginRequest.Username}");

                // Log indholdet af loginDto-objektet
                _logger.LogInformation($"Received login data: {JsonConvert.SerializeObject(loginRequest)}");

                // Din login logik her...
                var user = await _userService.GetUserByUsernameAndPassword(loginRequest.Username, loginRequest.Password);

                if (user != null)
                {
                    // Brugeren blev fundet, så send en succesrespons
                    _logger.LogInformation("Login successful");

                    // Returner kun de nødvendige data, herunder usertype
                    var response = new
                    {
                        username = user.User_Name,
                        name = user.Name,
                        usertype = user.Usertype // Sørg for at denne linje er korrekt og returnerer den forventede værdi
                    };

                    return Ok(response);
                }
                else
                {
                    // Brugeren blev ikke fundet, send en fejlrespons
                    _logger.LogInformation("Invalid username or password");
                    return NotFound(new { message = "Invalid username or password" });
                }
            }
            catch (Exception ex)
            {
                // Log fejlbesked i tilfælde af en exception
                _logger.LogError(ex, "An error occurred during login");
                // Returner et passende svar til klienten
                return StatusCode(500, new { message = "An error occurred during login", error = ex.Message });
            }
        }
        [HttpPost("checkTemporaryPasswordExpiration")]
        public async Task<IActionResult> CheckTemporaryPasswordExpiration([FromBody] int userId)
        {
            var result = await _userService.CheckTemporaryPasswordExpiration(userId);
            return Ok(result);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var result = await _userService.ChangePassword(changePasswordDTO.UserId, changePasswordDTO.NewPassword, changePasswordDTO.ConfirmPassword);
            if (result)
            {
                return Ok("Password changed successfully.");
            }
            return BadRequest("Failed to change password.");
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

            // Return user with all necessary fields
            var response = new
            {
                user.User_Id,
                user.User_Name,
                user.Name,
                user.Last_name,
                user.Address,
                user.Zip_code,
                user.E_mail,
                user.Mobil_nr,
                user.Usertype // Ensure this field is included and correctly mapped
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                // Valider indgående data
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Opret bruger
                await _userService.createUserWithTemporaryPassword(user.User_Name, user.Name,
                                                 user.Last_name, user.Address, user.Zip_code,
                                                 user.E_mail, user.Mobil_nr, user.Usertype);

                // Returner en bekræftelsesbesked
                return Ok(new { message = "Brugeren er oprettet med succes." });
            }
            catch (Exception ex)
            {
                // Log fejl og returner en fejlbesked
                _logger.LogError(ex, "Fejl under oprettelse af bruger.");
                return StatusCode(500, new { message = "Der opstod en fejl under oprettelse af bruger." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO updateUserDto)
        {
            try
            {
                // Valider indgående data
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Opdater bruger
                await _userService.UpdateUser(id, updateUserDto);

                // Returner en bekræftelsesbesked eller opdaterede brugeroplysninger
                return Ok(new { message = "Brugeren er opdateret med succes." });
            }
            catch (Exception ex)
            {
                // Log fejl og returner en fejlbesked
                _logger.LogError(ex, "Fejl under opdatering af bruger.");
                return StatusCode(500, new { message = "Der opstod en fejl under opdatering af bruger." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Slet bruger
                var foundUser = await _userService.GetUserByID(id);
                if (foundUser != null)
                {
                    await _userService.DeleteUser(id);
                    // Returner en bekræftelsesbesked
                    return Ok(new { message = "Brugeren er slettet med succes." });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log fejl og returner en fejlbesked
                _logger.LogError(ex, "Fejl under sletning af bruger.");
                return StatusCode(500, new { message = "Der opstod en fejl under sletning af bruger." });
            }
        }

    }
}
