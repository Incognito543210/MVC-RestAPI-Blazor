using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {

   
        private readonly IRecipesService _recipeService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public RecipeController(IRecipesService recipeService, IMapper mapper, IUsersService usersService)

        {
            _recipeService = recipeService;
             _mapper = mapper;
            _usersService = usersService;
        }


  
        [HttpGet("AllRecipes")]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()

        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeService.GetRecipes());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }



        [HttpGet("{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int recipeId)
        {
            if (!_recipeService.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeService.GetRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(recipe);


        }

        [HttpGet("ingridients/{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Ingridient))]
        [ProducesResponseType(400)]

        public IActionResult GetIngridientByRecipe(int recipeId)
        {
            if (!_recipeService.IngredientsExistsOnRecipe(recipeId))
                return NotFound();

            var ingridients = _mapper.Map<List<IngridientDto>>(_recipeService.GetIngridientsByRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(ingridients);

        }

        [HttpGet("tags/{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]

        public IActionResult GetTagsByRecipe(int recipeId)
        {
            if (!_recipeService.TagsExistsOnRecipe(recipeId))
                return NotFound();

            var tagsMap = _mapper.Map<List<TagDto>>(_recipeService.GetTagsByRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(tagsMap);

        }

        [HttpGet("ByUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]

        public IActionResult GetRecipesByUser(int userId)
        {
            if (_recipeService.RecipeExistsOnUser(userId))
                return NotFound();

            var recipes = _mapper.Map<List<RecipeDto>>(_recipeService.GetRecipesbyUser(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(recipes);

        }


        [HttpPost("{userID}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe(int  userID, [FromBody]RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var recipe = _recipeService.GetRecipes().Where(u=>u.Title.Trim().ToLower()==recipeCreate.Title.TrimEnd().ToLower()).FirstOrDefault();

            if(recipe!=null)
            {
                {
                    ModelState.AddModelError("", "Przepis już istnieje");
                    return StatusCode(422, ModelState);
                }
            }


            var recipeMap = _mapper.Map<Recipe>(recipeCreate);


            recipeMap.User = _usersService.GetUser(userID);
            recipeMap.UserID = userID;

            if(!_recipeService.CreateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie zapisano przepis");

        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateRecipe(int recipeId, [FromBody] RecipeDto updateRecipe)
        {
            if (updateRecipe == null)
                return BadRequest(ModelState);

            if (recipeId != updateRecipe.RecipeID)
                return BadRequest(ModelState);

            if (!_recipeService.RecipeExists(recipeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipe>(updateRecipe);

            if(!_recipeService.UpdateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak przy zmianie przepis");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie zmodyfikowano przepis");



        }



    }
}
