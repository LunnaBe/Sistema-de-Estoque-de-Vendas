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
    }
}
