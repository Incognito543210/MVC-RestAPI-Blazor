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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]
        public IActionResult GetByID(int id)
        {
            var tag = _mapper.Map<Tag>(_tagRepository.GetTag(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tag);
        }
    }
}
