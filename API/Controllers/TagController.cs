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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]
        public IActionResult GetByID(int id)
        {
            var tag = _mapper.Map<Tag>(_tagServices.GetByID(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tag);
        }
    }
}
