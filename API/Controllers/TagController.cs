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
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTags()
        {
            var tags = _mapper.Map<List<Tag>>(_tagRepository.GetTags());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }

        [HttpGet("{tagID}")]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]
        public IActionResult GetTag(int tagID)
        {
            var tag = _mapper.Map<Tag>(_tagRepository.GetTag(tagID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tag);
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

            var tag = _tagRepository.GetTags()
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

            if (!_tagRepository.CreateTag(tagMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                return BadRequest(ModelState);
            }

            return Ok("Pomyślnie zapisano tag");
        }

        [HttpPut("{tagID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTag(int tagID, [FromBody] TagDto updatedTag)
        {
            if(updatedTag == null)
                return BadRequest(ModelState);

            if (tagID != updatedTag.TagID)
                return BadRequest(ModelState);

            if (!_tagRepository.TagExists(tagID))
                return NotFound();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tagMap = _mapper.Map<Tag>(updatedTag);

            if (!_tagRepository.UpdateTag(tagMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak przy zmianie tagu");
                return StatusCode(422, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("tagID")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTag(int tagID)
        {
            if(!_tagRepository.TagExists(tagID))
                return NotFound();

            var tagToDelete = _tagRepository.GetTag(tagID);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_tagRepository.DeleteTag(tagToDelete))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas usuwania tagu");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
