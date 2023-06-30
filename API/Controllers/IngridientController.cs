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
        private readonly IIngridientsService _ingridientService;
        private readonly IMapper _mapper;
        private readonly IRecipesService recipeService;

        public IngridientController(IIngridientsService ingridientService, IMapper mapper)
        {
            _ingridientService = ingridientService;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingridient>))]
        public IActionResult GetIngridients()

        {
            var ingridients = _mapper.Map<List<IngridientDto>>(_ingridientService.GetIngridients());
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
            if (!_ingridientService.IngridientExists(id))
                return NotFound();

            var ingridient = _mapper.Map<IngridientDto>(_ingridientService.GetIngridient(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(ingridient);


        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngrideint([FromBody] ICollection<IngridientDto> ingridientsCreate, [FromQuery]string recipeName)
        {       

            foreach( IngridientDto ingridient in ingridientsCreate)
           {
                if (ingridient == null)
                {
                    return BadRequest(ModelState);
                }

             

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var ingridientMap = _mapper.Map<Ingridient>(ingridient);

                if (!_ingridientService.CreateIngridient(ingridientMap, recipeName, ingridient.Quantity))
                {
                    ModelState.AddModelError("", "Coś poszło nie tak podczas zapisywania");
                    return StatusCode(500, ModelState);
                }


            }

            return Ok("Zakończono pomyślnie");

        }
       


    }
}
