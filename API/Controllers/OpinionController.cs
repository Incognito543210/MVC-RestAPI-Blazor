using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        public IActionResult GetOpinions()
        {
            var opinions = _mapper.Map<List<OpinionDto>>(_opinionServices.GetOpinions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(opinions);
        }

        [HttpGet("recipeID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOpinionsForRecipe(int recipeID)
        {
            if (!_opinionServices.OpinionExistsOnRecipe(recipeID))
                return NotFound();

            var opinion = _mapper.Map<List<OpinionDto>>(_opinionServices.GetOpinionsForRecipe(recipeID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(opinion);
        }

        [HttpGet("userID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOpinionsForUser(int userID)
        {
            if (!_opinionServices.OpinionExistsOnUser(userID))
                return NotFound();

            var opinion = _mapper.Map<List<OpinionDto>>(_opinionServices.GetOpinionsForUser(userID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(opinion);
        }

        [HttpPost("{userID},{recipeID}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateOpinion( int userID, int recipeID, [FromBody] OpinionDto opinionCreate)
        {
            if (opinionCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var opinionMap = _mapper.Map<Opinion>(opinionCreate);

            opinionMap.User = _usersService.GetUser(userID);
            opinionMap.Recipe = _recipeRepository.GetRecipe(recipeID);

            if (!_opinionServices.CreateOpinion(opinionMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                return BadRequest(ModelState);
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
                return BadRequest(ModelState);

            if (opinionID != updatedOpinion.OpinionID)
                return BadRequest(ModelState);

            if (!_opinionServices.OpinionExists(opinionID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var opinionMap = _mapper.Map<Opinion>(updatedOpinion);

            if (!_opinionServices.UpdateOpinion(opinionMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak przy zmianie opinii");
                return BadRequest(ModelState);
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
                return NotFound();

            var opinionToDelete = _opinionServices.GetOpinion(opinionID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_opinionServices.DeleteOpinion(opinionToDelete))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas usuwania opinii");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie usunięto opinię"); //nie powinno być NoContent ???
        }
    }
}
