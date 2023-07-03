using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagsService _tagsService;
        private readonly IMapper _mapper;
        private readonly IHasCategoriesService _hasCategoriesService;

        public TagController(ITagsService tagsService, IMapper mapper, IHasCategoriesService hasCategoriesService)
        {
            _tagsService = tagsService;
            _mapper = mapper;
            _hasCategoriesService = hasCategoriesService;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTags()
        {
            var tags = _mapper.Map<List<Tag>>(_tagsService.GetTags());

            if (!ModelState.IsValid)
            {
                return BadRequest("Coś poszło nie tak");
            }
           
            return Ok(tags);
        }
        [AllowAnonymous]
        [HttpGet("{tagID}")]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]
        public IActionResult GetByID(int tagID)
        {
            var tag = _mapper.Map<Tag>(_tagsService.GetTag(tagID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tag);
        }

        [HttpPost("{recipeName}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult AddTagsForRecipe([FromBody] ICollection<TagDto> tagsAdd, string recipeName)
        {
            foreach (TagDto tagAdd in tagsAdd)
            {
                if (tagAdd == null)
                {
                    return BadRequest("Tag nie może być pusty");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Coś poszło nie tak");
                }

                var tagMap = _mapper.Map<Tag>(tagAdd);

                if (!_tagsService.AddTagsForRecipe(tagMap, recipeName))
                {
                    return StatusCode(500, "Coś poszło nie tak podczas zapisywania");
                }
            }

            return Ok("Zakończono pomyślnie");


        }


        [HttpPut("{recipeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateTags(int recipeId, [FromBody] ICollection<TagDto> tagsUpdate)
        {
            var hasCategoryToDelete = _hasCategoriesService.GetHasCategoriesByRecipe(recipeId);
            if (!_hasCategoriesService.DeleteTagsForRecipe(hasCategoryToDelete.ToList()))
            {
                return StatusCode(422, "Coś poszło nie tak z usuwaniem tagów przepisu");
            }

            foreach (TagDto tagUpdate in tagsUpdate)
            {

                if (tagUpdate == null)
                    return BadRequest("Tag nie może być pusty");

                if (!ModelState.IsValid)
                    return BadRequest("Coś poszło nie tak");

                var tagMap = _mapper.Map<Tag>(tagUpdate);

                if (!_tagsService.UpdateTagsForRecipe(tagMap, recipeId))
                {
                    return BadRequest("Coś poszło nie tak przy modyfikacji tagów");
                }

            }
            return Ok("Pomyślnie zmodyfikowano tagi");


        }


    }
}
