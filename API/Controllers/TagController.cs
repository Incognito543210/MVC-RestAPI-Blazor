using API.Interfaces;
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
        private readonly ITagServices _tagServices;
        private readonly IMapper _mapper;

        public TagController(ITagServices tagServices, IMapper mapper)
        {
            _tagServices = tagServices;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTags()
        {
            var tags = _mapper.Map<List<Tag>>(_tagServices.GetAllTags());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            return Ok(tags);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateTag([FromBody] TagDto tagCreate)
        {
            if (tagCreate == null)
            {
                return BadRequest(ModelState);
            }

            var tag = _tagServices.GetAllTags()
                .Where(t => t.Name.Trim().ToUpper() == tagCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (tag != null)
            {
                ModelState.AddModelError("", "Tag już istnieje");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tagMap = _mapper.Map<Tag>(tagCreate);

            if (!_tagServices.CreateTag(tagMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie zapisano tag");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTag( [FromBody] TagDto updatedTag)
        {
            if(updatedTag == null)
                return BadRequest();

            var tagMap = _mapper.Map<Tag>(updatedTag);

            if (!_tagServices.TagExists(tagMap))
                return NotFound();

            if (!_tagServices.UpdateTag(tagMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak przy zmianie tagu");
                return StatusCode(422, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTag(int id)
        {
            if(!_tagServices.TagExists(id))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_tagServices.DeleteTag(id))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas usuwania tagu");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
