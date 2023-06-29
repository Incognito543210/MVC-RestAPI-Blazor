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
        private readonly IIngridientsService _ingridientRepository;
        private readonly IMapper _mapper;
        private readonly IRecipesService recipeRepository;

        public IngridientController(IIngridientsService ingridientRepository, IMapper mapper)
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

       /* [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngrideint([FromBody] IngridientDto ingridientCreate)
        {
            if (ingridientCreate == null)
            {
                return BadRequest(ModelState);
            }

            var ingridient = _ingridientRepository.GetIngridients().Where(c => c.name.Trim().ToUpper() == ingridientCreate.name.TrimEnd().ToUpper()).FirstOrDefault();

            if(ingridient != null)
            {
                ModelState.AddModelError("", "Składnik już istnieje");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ingrideintMap = _mapper.Map<Ingridient>(ingridientCreate);


            if(!_ingridientRepository.CreateIngridient(ingrideintMap))
            {
                ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                return StatusCode(500, ModelState);
            }

            return Ok("Zakończono pomyślnie");

        }*/



    }
}
