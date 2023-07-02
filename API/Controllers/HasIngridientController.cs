using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

namespace API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class HasIngridientController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHasIngridientService _hasIngridientService;

        public HasIngridientController(IMapper mapper, IHasIngridientService hasIngridientService)
        {
            _mapper = mapper;
            _hasIngridientService = hasIngridientService;   
        }

        [HttpGet("{recipeId},{ingridentId}")]
        [ProducesResponseType(200, Type = typeof(String))]
        [ProducesResponseType(400)]

        public IActionResult GetAmountByRecipeAndIngridient(int recipeId, int ingridentId)
        {

            if (!_hasIngridientService.HasIngridientByRecipeAndIngridientExists(recipeId, ingridentId))
                return NotFound();

            var hasIngridient = _mapper.Map<HasIngridientDto>(_hasIngridientService.GetHasIngridientByRecipeAndIngridient(recipeId, ingridentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(hasIngridient);
        }


        [HttpDelete("{recipeId},{ingridientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteHasIngridient(int recipeId, int ingridientId)
        {
            if (!_hasIngridientService.HasIngridientByRecipeAndIngridientExists(recipeId, ingridientId))
                return NotFound();

            var HasIngridientToDelate = _hasIngridientService.GetHasIngridientByRecipeAndIngridient(recipeId, ingridientId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_hasIngridientService.DelateHasIngridient(HasIngridientToDelate))
            {            
                return BadRequest("Coś poszło nie tak podczas usuwania składnika");
            }

            return NoContent();
        }
    }
}
