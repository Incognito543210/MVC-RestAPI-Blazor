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

            if (!_hasIngridientService.AmountByRecipeAndIngridientExists(recipeId, ingridentId))
                return NotFound();

            var amount = _mapper.Map<HasIngridientDto>(_hasIngridientService.getAmountByRecipeAndIngridient(recipeId, ingridentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(amount);
        }

        
    }
}
