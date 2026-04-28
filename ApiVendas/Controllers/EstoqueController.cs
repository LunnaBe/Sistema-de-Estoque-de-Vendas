using ApiVendas.Config;
using ApiVendas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiVendas.Controllers
{
    [ApiController]
    [Route("api/v1/estoque")]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOptions<ApiConfig> _config;

        public EstoqueController(AppDbContext context, IOptions<ApiConfig> config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Listar()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Os parâmetros da requisição são inválidos.");
                }
                if (_context.Estoque == null)
                {
                    return NotFound("Nenhum dado de sensor foi encontrado.");
                }

                return Ok(_context.Estoque);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro interno no banco de dados.");
            }

        }
    }
}
