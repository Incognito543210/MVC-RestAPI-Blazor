using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionRepository _opinionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public OpinionController(IOpinionRepository opinionRepository, IUserRepository userRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _opinionRepository = opinionRepository;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        public IActionResult GetOpinions()
        {
            var opinions = _mapper.Map<List<OpinionDto>>(_opinionRepository.GetOpinions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(opinions);
        }

        [HttpGet("recipeID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        public IActionResult GetOpinionsForRecipe(int recipeID)
        {
            if (!_opinionRepository.OpinionExistsOnRecipe(recipeID))
                return NotFound();

            var opinion = _mapper.Map<List<OpinionDto>>(_opinionRepository.GetOpinionsForRecipe(recipeID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(opinion);
        }

        [HttpGet("userID")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        [ProducesResponseType(400)]
        public IActionResult GetOpinionsForUser(int userID)
        {
            if (!_opinionRepository.OpinionExistsOnUser(userID))
                return NotFound();

            var opinion = _mapper.Map<List<OpinionDto>>(_opinionRepository.GetOpinionsForUser(userID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(opinion);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOpinion([FromQuery] int userID, int recipeID, [FromBody] OpinionDto opinionCreate)
        {
            if (opinionCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var opinionMap = _mapper.Map<Opinion>(opinionCreate);

            opinionMap.User = _userRepository.GetUser(userID);
            opinionMap.Recipe = _recipeRepository.GetRecipe(recipeID);

            if (!_opinionRepository.CreateOpinion(opinionMap))
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
        public IActionResult UpdateOpinion(int opinionID, [FromBody]OpinionDto updatedOpinion)
        {
            if(updatedOpinion == null)
                return BadRequest(ModelState);

            if(opinionID !=updatedOpinion.OpinionID)
                return BadRequest(ModelState);
            
            if(!_opinionRepository.OpinionExists(opinionID))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var opinionMap = _mapper.Map<Opinion>(updatedOpinion);

            if(!_opinionRepository.UpdateOpinion(opinionMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak przy zmianie opinii");
            }

            return Ok("Pomyślnie zaktualizowano opinię");
        }
    }
}
