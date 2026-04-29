using ApiVendas.Config;
using ApiVendas.Data;
using ApiVendas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared;

namespace ApiVendas.Controllers
{
    [ApiController]
    [Route("api/v1/estoque")]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EstoqueService _service;
        private readonly IOptions<ApiConfig> _config;

        public EstoqueController(AppDbContext context, IOptions<ApiConfig> config, EstoqueService service)
        {
            _context = context;
            _service = service;
            _config = config;
        }

        /// <summary>
        /// GET: api/v1/estoque - Retorna uma lista de produtos de um estoque cadastrados no banco de dados
        /// </summary>
        /// 
        /// <remarks> 
        /// Uma requisição que retorna valores de um produto, como: Id, Codigo_Fornecedor, Nome_Produto, Quantidade, Preço, Data_Entrada, Data_Saida. 
        /// 
        /// Observação: O endpoint deve retornar uma lista de valores, 
        /// ou um status de erro apropriado se nenhum valor for encontrado ou se a requisição for inválida.
        /// </remarks>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Leituras dos produtos encontradas com sucesso</response>
        /// <response code="204">Nenhum dado de produto disponível no momento</response>
        /// <response code="400">Requisição inválida ou parâmetros malformados</response>
        /// <response code="404">Nenhum registro de produto encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarEstoque()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Os parâmetros da requisição são inválidos.");
                }
                if (_context.Estoque == null)
                {
                    return NotFound("Nenhum produto foi encontrado no estoque.");
                }

                return Ok(_context.Estoque);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno no banco de dados.");
            }
        }

        /// <summary>
        /// GET: api/v1/estoque/{id} - Retorna um produto específico do estoque por ID
        /// </summary>
        /// 
        /// <remarks>
        /// Retorna um produto específico do estoque com base no ID fornecido.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Produto encontrado com sucesso</response>
        /// <response code="204">Nenhum dado de produto disponível no momento</response>
        /// <response code="400">Requisição inválida ou parâmetros malformados</response>
        /// <response code="404">Nenhum registro de produto encontrado</response>
        /// <response code="409">Conflito ao processar os dados dos produtos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var produto = await _service.ObterPorId(id);

                if (id <= 0)
                {
                    return BadRequest("Id deve ser um número inteiro positivo.");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Os parâmetros da requisição são inválidos.");
                }
                if (_context.Estoque == null)
                {
                    return NotFound("Nenhum produto foi encontrado no estoque.");
                }

                if (produto == null)
                {
                    return NotFound("Nenhum produto foi encontrado no estoque.");
                }

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno no banco de dados.");
            }

        }

        /// <summary>
        /// POST: api/v1/estoque - Cria um novo produto do estoque no banco de dados
        /// </summary>
        /// 
        /// <remarks>
        /// Requisitos de validação:
        /// - Id: obrigatório, deve ser um número inteiro positivo
        /// - Codigo_Fornecedor: obrigatório, deve ser uma string não vazia
        /// - Nome_Produto: obrigatório, deve ser uma string não vazia
        /// - Quantidade: obrigatório, deve ser um número inteiro positivo
        /// - Preço: obrigatório, deve ser um número decimal, máximo de 2 casas decimais (ex: 40.00)
        /// - Data_Entrada: obrigatório, deve ser uma data e hora válida no formato ISO 8601 (ex: 2024-06-01T12:00:00Z)
        /// - Data_Saida: opcional, deve ser uma data e hora válida no formato ISO 8601 (ex: 2024-06-01T12:00:00Z)
        /// 
        /// 
        /// Observação: O endpoint deve retornar um status de sucesso (200 OK) se o produto for criado com sucesso, 
        /// ou um status de erro apropriado se a criação falhar devido a dados inválidos ou outros problemas.
        /// </remarks>
        /// 
        /// <param name="produto"></param>
        /// 
        /// <returns></returns> 
        /// 
        /// <response code="200">Leituras dos produtos encontradas com sucesso</response>
        /// <response code="204">Nenhum dado de produto disponível no momento</response>
        /// <response code="400">Requisição inválida ou parâmetros malformados</response>
        /// <response code="404">Nenhum registro de produto encontrado</response>
        /// <response code="409">Conflito ao processar os dados dos produtos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarProduto([FromBody] EstoqueData produto)
        {
            try
            {

                if (produto.Quantidade > _config.Value.MaxQuantidade)
                {
                    return BadRequest("Quantidade acima do máximo permitido!");
                }

                if (produto.Quantidade < _config.Value.LimiteQuantidade)
                {
                    return BadRequest("Quantidade abaixo do limite permitido!");
                }

                if (produto.Id <= 0)
                {
                    return BadRequest("Id deve ser um número inteiro positivo.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Os parâmetros da requisição são inválidos.");
                }
                if (produto == null)
                {
                    return NotFound("Nenhum produto foi encontrado no estoque.");
                }
                _context.Estoque.Add(produto);
                _context.SaveChanges();
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno no banco de dados.");
            }
        }


        /// <summary>
        /// PUT: api/v1/estoque/{id} - Atualiza um produto do estoque pelo ID
        /// </summary>
        /// 
        /// <remarks> 
        /// Uma requisição que retorna valores atualizados de um produto, como: Id, Codigo_Fornecedor, Nome_Produto, Quantidade, Preço, Data_Entrada, Data_Saida. 
        /// 
        /// Observação: O endpoint deve retornar os valores atualizados do produto,
        /// ou um status de erro apropriado se nenhum valor for encontrado ou se a requisição for inválida.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Produto atualizado com sucesso</response>
        /// <response code="204">Nenhum dado de produto disponível no momento</response>
        /// <response code="400">Requisição inválida ou parâmetros malformados</response>
        /// <response code="404">Nenhum registro de produto encontrado</response>
        /// <response code="409">Conflito ao processar os dados dos produtos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] EstoqueData produto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id deve ser um número inteiro positivo.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Os parâmetros da requisição são inválidos.");
                }
                if (produto == null)
                {
                    return NotFound("Nenhum produto foi encontrado no estoque.");
                }

                await _service.Atualizar(produto);
                return Ok(_context.Estoque);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno no banco de dados.");
            }

        }

        /// <summary>
        /// DELETE: api/v1/estoque/{id} - Remove um produto do estoque pelo ID
        /// </summary>
        /// 
        /// <remarks> 
        /// Uma requisição que remove um produto do estoque pelo ID fornecido.
        /// 
        /// Observação: O endpoint deve retornar um status de sucesso se o produto for removido com sucesso,
        /// ou um status de erro apropriado se nenhum produto for encontrado ou se a requisição for inválida.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// 
        /// <returns></returns>
        /// 
        /// <response code="200">Produto removido com sucesso</response>
        /// <response code="204">Nenhum dado de produto disponível no momento</response>
        /// <response code="400">Requisição inválida ou parâmetros malformados</response>
        /// <response code="404">Nenhum registro de produto encontrado</response>
        /// <response code="409">Conflito ao processar os dados dos produtos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarProduto(int id, [FromBody] EstoqueData produto)
        {
            try
            {
                var produtoExistente = await _service.ObterPorId(id);
                if (!ModelState.IsValid)
                {
                    return BadRequest("Os parâmetros da requisição são inválidos.");
                }
                if (_context.Estoque == null)
                {
                    return NotFound("Nenhum produto foi encontrado no estoque.");
                }

                await _service.Deletar(id);
                return Ok(_context.Estoque);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno no banco de dados.");
            }

        }
    }
}
