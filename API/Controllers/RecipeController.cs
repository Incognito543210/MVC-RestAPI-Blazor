using API.Interfaces;
using API.Repositories;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {

   
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)

        {
         
            _recipeRepository = recipeRepository;
        }

<<<<<<< HEAD
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetRecipebyId([FromRoute] int id)
=======
       
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()
>>>>>>> 27dad0ce76c6ebdd8ec12d82019b6b02ffd2c289
        {
            var recipes = _recipeRepository.GetRecipes();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }

    }
}
