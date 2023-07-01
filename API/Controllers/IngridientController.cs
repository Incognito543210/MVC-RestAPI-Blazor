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



        [HttpGet("{ingridientId}")]
        [ProducesResponseType(200, Type = typeof(Ingridient))]
        [ProducesResponseType(400)]
        public IActionResult GetIngridient(int ingridientId)
        {
            if (!_ingridientService.IngridientExists(ingridientId))
                return NotFound();

            var ingridient = _mapper.Map<IngridientDto>(_ingridientService.GetIngridient(ingridientId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(ingridient);


        }

        [HttpPost("{recipeName}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngrideint([FromBody] ICollection<IngridientDto> ingridientsCreate, string recipeName)
        {

            foreach (IngridientDto ingridientCreate in ingridientsCreate)
            {
                if (ingridientCreate == null)
                {
                    return BadRequest(ModelState);
                }



                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var ingridientMap = _mapper.Map<Ingridient>(ingridientCreate);

                if (!_ingridientService.CreateIngridient(ingridientMap, recipeName, ingridientCreate.Quantity))
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

        public IActionResult UpdateIngridenits(int recipeId, [FromBody] ICollection<IngridientDto> ingridientsUpdate)
        {
            foreach(IngridientDto ingridientUpdate in ingridientsUpdate)
            {

                if (ingridientUpdate == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ingridientMap = _mapper.Map<Ingridient>(ingridientUpdate);

                if(!_ingridientService.UpdateIngridient(ingridientMap,recipeId,ingridientUpdate.Quantity))
                {
                    ModelState.AddModelError("", "Coś poszło nie tak przy zmianie składnika");
                    return BadRequest(ModelState);
                }

            }
            return Ok("Pomyślnie zmodyfikowano składniki");


        }

    }
}
