using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.MODEL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngridientController : ControllerBase
    {
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IMapper _mapper;

        public IngridientController(IIngridientRepository ingridientRepository, IMapper mapper)
        {
            _ingridientRepository = ingridientRepository;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingridient>))]
        public IActionResult GetIngridients()

        {
            var ingridients = _mapper.Map<List<IngridientDto>>(_ingridientRepository.GetIngridients());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(ingridients);
        }



        [HttpGet("{IngridientId}")]
        [ProducesResponseType(200, Type = typeof(Ingridient))]
        [ProducesResponseType(400)]
        public IActionResult GetIngridient(int id)
        {
            if (!_ingridientRepository.IngridientExists(id))
                return NotFound();

            var ingridient = _mapper.Map<IngridientDto>(_ingridientRepository.GetIngridient(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(ingridient);


        }



    }
}
