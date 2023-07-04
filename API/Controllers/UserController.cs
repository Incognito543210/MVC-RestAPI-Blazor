using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;
using System.Diagnostics.Eventing.Reader;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _userServices;
        private readonly IMapper _mapper;

        public UserController(IUsersService userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpGet("{userID}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int userID)
        {
            if (!_userServices.UserExists(userID))
                return NotFound();

            var recipes = _mapper.Map<User>(_userServices.GetUser(userID));

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak podczas zapisywania");
            }

            return Ok(recipes);
        }

        [AllowAnonymous]
        [HttpGet("{login},{password}")]
        [ProducesResponseType(200, Type = typeof(SessionDto))]
        public IActionResult Login(string login, string password)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Wystąpił jakiś błąd");
            }

            if (login is null)
                return BadRequest("Login nie może być pusty");

            if (password is null)
                return BadRequest("Hasło nie może być puste");

            if (_userServices.EmailExists(login) || _userServices.UsernameExists(login))
            {
                var session = _userServices.Logger(login, password);
                if (session is null)
                    return StatusCode(422, "Nieprawidłowa nazwa użytkownika, adres e-mail lub hasło.");
                return Ok(session);
            }
            else
            {
                return StatusCode(422, "Nieprawidłowa nazwa użytkownika lub adresu e-mail");
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_userServices.UsernameExists(userDto.Username))
            {
                return StatusCode(422, "Nazwa użytkownika jest już zajęta.");
            }

            if (_userServices.EmailExists(userDto.Email))
            {
                return StatusCode(422, "Adres e-mail jest już zajęty.");
            }

            var userMap = _mapper.Map<User>(userDto);

            if (!_userServices.IsEmailValid(userMap.Email))
            {
                return StatusCode(422, "Nieprawidłowy adres e-mail.");
            }

            if (!_userServices.IsPasswordStrong(userMap.Password))
            {
                return StatusCode(422, "Hasło jest niewystarczająco mocne.");
            }

            if (_userServices.IsPasswordPopular(userMap.Password))
            {
                return StatusCode(422, "Hasło jest zbyt popularne.");

            }

            if (!_userServices.CreateUser(userMap))
            {
                return StatusCode(422, "Rejestracja konta nie powiodła się.");
            }
            return Ok();
        }

        [HttpPut("userID")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userID, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (!_userServices.UserExists(userID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _userServices.GetUsers()
                .Where(u => u.Username.Trim().ToUpper() == updatedUser.Username.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (users != null)
            {
                return StatusCode(422, "Użytkownik już istnieje.");

            }

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userServices.IsEmailValid(userMap.Email))
            {
                return StatusCode(422, "Nieprawidłowy adres e-mail.");
            }

            if (!_userServices.IsPasswordStrong(userMap.Password))
            {
                return StatusCode(422, "Hasło jest niewystarczająco mocne.");
            }

            if (_userServices.IsPasswordPopular(userMap.Password))
            {
                return StatusCode(422, "Hasło jest zbyt popuarne.");
            }

            if (!_userServices.UpdateUser(userMap))
            {
                return BadRequest("Zmiana danych użytkownika nie powiodła się.");
            }

            return Ok("Zmiana danych użytkownika przebiegła pomyślnie.");
        }
    }
}
