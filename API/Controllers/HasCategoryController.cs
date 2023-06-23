using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HasCategoryController : ControllerBase
    {
        private readonly IHasCategoryRepository _hasCategoryRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public HasCategoryController(IHasCategoryRepository hasCategoryRepository, IRecipeRepository recipeRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _hasCategoryRepository = hasCategoryRepository;
            _recipeRepository = recipeRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet("{recipeID}")]
        [ProducesResponseType(200, Type = typeof(HasCategory))]
        [ProducesResponseType(400)]
        public IActionResult GetHasCategoryForRecipe(int recipeID)
        {
            var hasCategory = _mapper.Map<HasCategory>(_hasCategoryRepository.GetHasCategoryForRecipe(recipeID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(hasCategory);
        }

        [HttpGet("{tagID}")]
        [ProducesResponseType(200, Type = typeof(HasCategory))]
        [ProducesResponseType(400)]
        public IActionResult GetHasCategoryForTag(int tagID)
        {
            var hasCategory = _mapper.Map<HasCategory>(_hasCategoryRepository.GetHasCategoryForTag(tagID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(hasCategory);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateHasCategory([FromQuery] int recipeID, int tagID, [FromBody] HasCategoryDto hasCategoryCreate)
        {
            if (hasCategoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hasCategoryMap = _mapper.Map<HasCategory>(hasCategoryCreate);

            hasCategoryMap.Recipe = _recipeRepository.GetRecipe(recipeID);
            hasCategoryMap.Tag = _tagRepository.GetTag(tagID);

            if (!_hasCategoryRepository.CreateHasCategory(hasCategoryMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak z tworzeniem przydziału tagu do przepisu");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie utworzono przydział tagu do przepisu");
        }

        [HttpDelete("tagID")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHasCategoryByTag(int tagID)
        {
            if (!_hasCategoryRepository.HasCategoryExistsByTag(tagID))
                return NotFound();

            var hasCategoryToDelete = _hasCategoryRepository.GetHasCategoryForTag(tagID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hasCategoryRepository.DeleteHasCategory(hasCategoryToDelete))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas próby usunięcia");
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("recipeID")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteHasCategoryByRecipe(int recipeID)
        {
            if (!_hasCategoryRepository.HasCategoryExistsByRecipe(recipeID))
                return NotFound();

            var hasCategoryToDelete = _hasCategoryRepository.GetHasCategoryForRecipe(recipeID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hasCategoryRepository.DeleteHasCategory(hasCategoryToDelete))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas usunięcia");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
