using API.Interfaces;
using API.Repositories;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IHasIngridientService _hasIngridientService;

        public IngridientController(IIngridientsService ingridientService, IMapper mapper, IHasIngridientService hasIngridientService)
        {
            _ingridientService = ingridientService;
            _mapper = mapper;
            _hasIngridientService = hasIngridientService;
        }

        [AllowAnonymous]
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


        [AllowAnonymous]
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
                    return StatusCode(500, "Coś poszło nie tak podczas zapisywania");
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
            var hasIngridientToDelete = _hasIngridientService.GetHasIngridientsByRecipe(recipeId);
            if (!_hasIngridientService.DeleteIngridientsForRecipe(hasIngridientToDelete.ToList()))
            {               
                return BadRequest("Coś poszło nie tak z usówaniem składnikami przepisu");
            }

            foreach (IngridientDto ingridientUpdate in ingridientsUpdate)
            {

                if (ingridientUpdate == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ingridientMap = _mapper.Map<Ingridient>(ingridientUpdate);

                if(!_ingridientService.UpdateIngridient(ingridientMap,recipeId,ingridientUpdate.Quantity))
                {
                    return BadRequest("Coś poszło nie tak przy zmianie składnika");
                }

            }
            return Ok("Pomyślnie zmodyfikowano składniki");


        }

    }
}
