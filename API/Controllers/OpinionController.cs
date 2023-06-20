using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionRepository _opinionRepository;

        public OpinionController(IOpinionRepository opinionRepository)
        {
            _opinionRepository = opinionRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opinion>))]
        public IActionResult GetOpinions()
        {
            var opinions = _opinionRepository.GetOpinions();

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(opinions);
        }
    }
}
