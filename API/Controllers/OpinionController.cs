using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionsService _opinionServices;
        private readonly IUsersService _usersService;
        private readonly IRecipesService _recipeRepository;
        private readonly IMapper _mapper;

        public OpinionController(IOpinionsService opinionServices, IUsersService usersService, IRecipesService recipeRepository, IMapper mapper)
        {
            _opinionServices = opinionServices;
            _usersService = usersService;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        public IActionResult GetOpinions()
        {
            var opinions = _mapper.Map<List<OpinionDto>>(_opinionServices.GetOpinions());

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
            return Ok(opinions);
        }

        [HttpGet("{recipeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOpinionsForRecipe(int recipeID)
        {
            if (!_opinionServices.OpinionExistsOnRecipe(recipeID))
            {
                var errorMessage = "Nie znaleziono opinii";
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent(errorMessage);
                response.Headers.Add("Error-Message", errorMessage);

                return NotFound(response);
            }

            var opinion = _mapper.Map<List<OpinionDto>>(_opinionServices.GetOpinionsForRecipe(recipeID));

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            return Ok(opinion);
        }

        [AllowAnonymous]
        [HttpGet("userID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOpinionsForUser(int userID)
        {
            if (!_opinionServices.OpinionExistsOnUser(userID))
                return NotFound("Nie znaleziono opinii");

            var opinion = _mapper.Map<List<OpinionDto>>(_opinionServices.GetOpinionsForUser(userID));

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            return Ok(opinion);
        }

        [HttpPost("{userID},{recipeID}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateOpinion( int userID, int recipeID, [FromBody] OpinionDto opinionCreate)
        {
            if (opinionCreate == null)
                return BadRequest("Opinia nie może być pusta");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            var opinionMap = _mapper.Map<Opinion>(opinionCreate);

            opinionMap.User = _usersService.GetUser(userID);
            opinionMap.Recipe = _recipeRepository.GetRecipe(recipeID);

            if (!_opinionServices.CreateOpinion(opinionMap))
            {
                return BadRequest("Coś poszło nie tak podczas zapisywania");
            }

            return Ok("Pomyślnie zapisano opinię");
        }

        [HttpPut("{opinionID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOpinion(int opinionID, [FromBody] OpinionDto updatedOpinion)
        {
            if (updatedOpinion == null)
                return BadRequest("Opinia jest pusta");

            if (opinionID != updatedOpinion.OpinionID)
                return BadRequest("ID Opinii nie zgadza się z ID poprzedniej opinii");

            if (!_opinionServices.OpinionExists(opinionID))
                return NotFound("Nie znaleziono opinii");

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            var opinionMap = _mapper.Map<Opinion>(updatedOpinion);

            if (!_opinionServices.UpdateOpinion(opinionMap))
            {
                return BadRequest("Coś poszło nie tak przy zmianie opinii");
            }

            return Ok("Pomyślnie zmodyfikowano opinię");
        }

        [HttpDelete("{opinionID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOpinion(int opinionID)
        {
            if (!_opinionServices.OpinionExists(opinionID))
                return NotFound("Nie znaleziono opinii");

            var opinionToDelete = _opinionServices.GetOpinion(opinionID);

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_opinionServices.DeleteOpinion(opinionToDelete))
            {
                return BadRequest("Coś poszło nie tak podczas usuwania opinii");
            }

            return Ok("Pomyślnie usunięto opinię"); //nie powinno być NoContent ???
        }
    }
}
