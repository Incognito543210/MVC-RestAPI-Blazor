using API.Interfaces;
using API.Services;
using AutoMapper;
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

        public TagController(ITagsService tagsService, IMapper mapper)
        {
            _tagsService = tagsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTags()
        {
            var tags = _mapper.Map<List<Tag>>(_tagsService.GetTags());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            return Ok(tags);
        }

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
                    return BadRequest(ModelState);
                }



                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var tagMap = _mapper.Map<Tag>(tagAdd);

                if (!_tagsService.AddTagsForRecipe(tagMap, recipeName))
                {
                    ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                    return StatusCode(500, ModelState);
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
            foreach (TagDto tagUpdate in tagsUpdate)
            {

                if (tagUpdate == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tagMap = _mapper.Map<Tag>(tagUpdate);

                if (!_tagsService.UpdateTagsForRecipe(tagMap, recipeId))
                {
                    ModelState.AddModelError("", "Coś poszło nie tak przy zmianie tagu");
                    return BadRequest(ModelState);
                }

            }
            return Ok("Pomyślnie zmodyfikowano tagi");


        }


    }
}
