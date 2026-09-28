using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// Agregar usings para interfaz y Dominio
using PizzeriaRepository.Data.Interfaces;
using PizzeriaRepository.Domain;
using PizzeriaRepository.Services;

namespace PizzeriaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaService _service;

        // Inyección de dependencias a través del constructor
        public PizzasController(PizzaService service)
        {
            _service = service;
        }

        // GET: api/pizzas
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzas = await _service.ObtenertodasAsync();
            return Ok(pizzas);
        }

        // GET: api/pizzas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pizza = await _service.ObtenerPorIdAsync(id);
            if (pizza == null) return NotFound($"No se encontró la pizza con ID {id}");
            return Ok(pizza);
        }

        // POST: api/pizzas
        // Funciona como PUT si se le pasa id de Maestro, y (Id de Detalle + Id de Pizza) en cada detalle
        // IMPORTANTE cuando se hace un Post - No se olviden de Eliminar del Json: codigo (de pizza), codigo y codigo pizza (del ingrediente) -->fue lo que nos dió error en clase!
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pizza pizza)
        {
            try
            {
                var result = await _service.GuardarAsync(pizza);
                return Ok(new { message = "Pizza guardada exitosamente.", pizza });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DarDeBajaAsync(id);

            if (result)
                return Ok(new { message = "Pizza dada de baja correctamente." });

            return NotFound($"No se encontró la pizza con ID {id}");
        }

    }
}
