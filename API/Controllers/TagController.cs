using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        public IActionResult GetOpinions()
        {
            var tags = _tagRepository.GetTags();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(tags);
        }
    }
}
