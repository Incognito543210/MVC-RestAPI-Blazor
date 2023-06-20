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
        private readonly DataContext _context;
        public RecipeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetRecipebyId([FromRoute] int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(a => a.RecipeID == id);
            if (recipe is null)
            {
                return NotFound();
            }
            else
            {

                return Ok(recipe);
            }
        }
    }
}
