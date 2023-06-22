using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{userID}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int userID)
        {
            if (!_userRepository.UserExists(userID))
                return NotFound();

            var recipes = _mapper.Map<User>(_userRepository.GetUser(userID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipes);
        }

    }
}
