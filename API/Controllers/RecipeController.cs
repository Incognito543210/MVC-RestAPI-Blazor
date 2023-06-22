using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {

   
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)

        {
            _recipeRepository = recipeRepository;
             _mapper = mapper;
        }


  
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()

        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }



        [HttpGet("RecipeId")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(recipe);


        }    
    }
}
