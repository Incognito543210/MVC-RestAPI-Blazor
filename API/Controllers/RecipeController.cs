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


  
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()

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
