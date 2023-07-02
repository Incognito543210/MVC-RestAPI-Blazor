using API.Interfaces;
using API.Services;
using AutoMapper;
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
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

        [HttpGet("{login},{password}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult Login(string login, string password)
        {
            if (login is null)
                return BadRequest(ModelState);

            if (password is null)
                return BadRequest(ModelState);

            if (_userServices.EmailExists(login) || _userServices.UsernameExists(login))
            {
                var userMap = _mapper.Map<UserDto>(_userServices.Logger(login, password));
                userMap.Password = null;
                return Ok(userMap);
            }
            else
            {
                ModelState.AddModelError("", "Taki użytkownik nie istnieje");
                return StatusCode(422, ModelState);
            }

        }


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
                ModelState.AddModelError("", "Nazwa takiego użytkownika już istnieje");
                return StatusCode(422, ModelState);
            }

            if (_userServices.EmailExists(userDto.Email))
            {
                ModelState.AddModelError("", "Ten email już jest wykorzystany");
                return StatusCode(422, ModelState);
            }

            var userMap = _mapper.Map<User>(userDto);

            if (!_userServices.IsEmailValid(userMap.Email))
            {
                ModelState.AddModelError("", "Nieprawidłowy adres email");
                return StatusCode(422, ModelState);
            }

            if (!_userServices.IsPasswordStrong(userMap.Password))
            {
                ModelState.AddModelError("", "Hasło jest niewystarczająco mocne");
                return StatusCode(422, ModelState);
            }

            if (_userServices.IsPasswordPopular(userMap.Password))
            {
                ModelState.AddModelError("", "Hasło jest zbyt popularne");
                return StatusCode(422, ModelState);

            }

            if (!_userServices.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                return StatusCode(422, ModelState);
            }
            return Ok();
        }

        [HttpPut("userID")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOpinion(int userID, [FromBody] UserDto updatedUser)
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
                ModelState.AddModelError("", "Użytkownik już istnieje");
                return StatusCode(422, ModelState);

            }

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userServices.IsEmailValid(userMap.Email))
            {
                ModelState.AddModelError("", "Nieprawidłowy adres email");
                return StatusCode(422, ModelState);
            }

            if (!_userServices.IsPasswordStrong(userMap.Password))
            {
                ModelState.AddModelError("", "Hasło jest niewystarczająco mocne");
                return StatusCode(422, ModelState);
            }

            if (_userServices.IsPasswordPopular(userMap.Password))
            {
                ModelState.AddModelError("", "Hasło jest zbyt popularne");
                return StatusCode(422, ModelState);
            }

            if (!_userServices.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak przy zmianie danych użytkownika");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie zmodyfikowano dane użytkownika");
        }
    }
}
