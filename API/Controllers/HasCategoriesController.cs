﻿using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpDelete("{recipeId},{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteHasCategory(int recipeId, int categoryId)
        {
            if (!_hasCategoryService.HasCategorytByRecipeAndTagExists(recipeId, categoryId))
                return NotFound("Nie znaleziono połączenia");

            var HasCategoryToDelate = _hasCategoryService.GetHasCategoryByRecipeAndTag(recipeId, categoryId);

            if (!ModelState.IsValid)
                return BadRequest("Coś poszło nie tak");

            if (!_hasCategoryService.DelateHasCategory(HasCategoryToDelate))
            {
                return BadRequest("Coś poszło nie tak podczas usuwania kategori");
            }

            return NoContent();
        }



    }
}
