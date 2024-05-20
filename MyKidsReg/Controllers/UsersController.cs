﻿using Microsoft.AspNetCore.Cors;
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
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            try
            {
                // Log besked før login-forsøget
                _logger.LogInformation($"Attempting to login with username: {loginDto.Username}");

                // Log indholdet af loginDto-objektet
                _logger.LogInformation($"Received login data: {JsonConvert.SerializeObject(loginDto)}");

                // Din login logik her...
                var user = await _userService.GetUserByUsernameAndPassword(loginDto.Username, loginDto.Password);

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


        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.createUserWithTemporaryPassword(user.User_Name, user.Name,
                                             user.Last_name, user.Address, user.Zip_code,
                                             user.E_mail, user.Mobil_nr, user.Usertype);

            return CreatedAtAction(nameof(GetUserById), new { id = user.User_Id }, user);
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult >UpdateUser(int id, User user)
        {
            if(id != user.User_Id)
            {
                return BadRequest();
            }
          await _userService.UpdateUser(id, user);
            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundUser = await _userService.GetUserByID(id);
            if (foundUser != null)
            {
                await _userService.DeleteUser(id); 
                return NoContent();
            }
            return NotFound();
        }
    }
}
