using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HasCategoriesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IHasCategoriesService _hasCategoryService;
        public HasCategoriesController(IMapper mapper, IHasCategoriesService hasCategoryService)
        {
            _mapper = mapper;
            _hasCategoryService = hasCategoryService;
        }

        [HttpDelete("{recipeId},{ingridientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteHasCategory(int recipeId, int categoryId)
        {
            if (!_hasCategoryService.HasCategorytByRecipeAndTagExists(recipeId, categoryId))
                return NotFound();

            var HasCategoryToDelate = _hasCategoryService.GetHasCategoryByRecipeAndTag(recipeId, categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hasCategoryService.DelateHasCategory(HasCategoryToDelate))
            {
                
                return BadRequest("Coś poszło nie tak podczas usuwania kategori");
            }

            return NoContent();
        }



    }
}
