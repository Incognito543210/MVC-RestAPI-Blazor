using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IOpinionsService _opinionsService;
        private readonly IHasCategoriesService _hasCategoriesService;
        private readonly IHasIngridientService _hasIngridientService;

        public RecipeController(IRecipesService recipeService, IMapper mapper, IUsersService usersService, IOpinionsService opinionsService, IHasCategoriesService hasCategoriesService, IHasIngridientService hasIngridientService)

        {
            _recipeService = recipeService;
            _mapper = mapper;
            _usersService = usersService;
            _hasCategoriesService = hasCategoriesService;
            _hasIngridientService = hasIngridientService;
            _opinionsService = opinionsService;
        }


        [AllowAnonymous]
        [HttpGet("AllRecipes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()

        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeService.GetRecipes());
            if (!ModelState.IsValid)
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
            if (!_recipeService.RecipeExistsOnUser(userId))
                return NotFound();

            var recipes = _mapper.Map<List<RecipeDto>>(_recipeService.GetRecipesbyUser(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(recipes);

        }
        [AllowAnonymous]
        [HttpPost("ByTags")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]

        public IActionResult GetRecipesByTags([FromBody] ICollection<TagDto> tags)
        {

            var listOfTags = _mapper.Map<List<Tag>>(tags);

            if (!_recipeService.GetRecipesByTagsExists(listOfTags))
                return NotFound();

            var recipes = _mapper.Map<List<RecipeDto>>(_recipeService.GetRecipesByTags(listOfTags));
   

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
                    return StatusCode(422, "Przepis już istnieje");
                }
            }


            var recipeMap = _mapper.Map<Recipe>(recipeCreate);


            recipeMap.User = _usersService.GetUser(userID);
            recipeMap.UserID = userID;

            if(!_recipeService.CreateRecipe(recipeMap))
            {
                return BadRequest("Coś poszło nie tak podczas zapisywania");
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
                return BadRequest("Coś poszło nie tak przy zmianie przepis");
            }

            return Ok("Pomyślnie zmodyfikowano przepis");



        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteRecipe(int recipeId)
        {
            if(!_recipeService.RecipeExists(recipeId))
            {
                return NotFound();
            }

            var opinionsToDelete = _opinionsService.GetOpinionsForRecipe(recipeId);
            var hasIngridientToDelete = _hasIngridientService.GetHasIngridientsByRecipe(recipeId);
            var hasCategoryToDelete = _hasCategoriesService.GetHasCategoriesByRecipe(recipeId);
            var recipeToDelete = _recipeService.GetRecipe(recipeId);

            if(!_opinionsService.DeleteOpinionsForRecipe(opinionsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Coś poszło nie tak z usówaniem opini przepisu");
            }

            if (!_hasIngridientService.DeleteIngridientsForRecipe(hasIngridientToDelete.ToList()))
            {
                ModelState.AddModelError("", "Coś poszło nie tak z usówaniem składnikami przepisu");
            }

            if (!_hasCategoriesService.DeleteTagsForRecipe(hasCategoryToDelete.ToList()))
            {
                ModelState.AddModelError("", "Coś poszło nie tak z usówaniem tagami przepisu");
            }

            if(!_recipeService.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Coś poszło nie tak z usówaniem przepisu");
            }

            return Ok("Usunięto przepis pomyślnie");
        }


    }
}
