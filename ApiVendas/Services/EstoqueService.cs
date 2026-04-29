using ApiVendas.Repositories.Interfaces;
using Shared;

namespace ApiVendas.Services
{
    /// <summary>
    /// EstoqueService - classe de serviço responsável
    /// por toda a lógica de negócios relacionada a produtos no estoque
    /// </summary>
    public class EstoqueService
    {
        /// <summary>
        /// repository de estoque - responsável por acessar os 
        /// dados do estoque no banco de dados
        /// </summary> 

        private readonly IEstoqueRepository _repo;

        /// <summary>
        /// construtor da classe - recebe o repository de estoque
        /// via injeção de dependência
        /// </summary>
        /// <param name="repo"></param>

        public EstoqueService(IEstoqueRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// listar todos os produtos existentes no estoque - chama o metódo GetAll do 
        /// repository para obter a lista de produtos do banco de dados
        /// </summary>
        /// <returns></returns>

        public async Task<List<EstoqueData>> Listar() => await _repo.GetAll();

        /// <summary>
        /// obter um produto do estoque por id - chama o método GetById
        /// do repository para obter um produto específico 
        /// do banco de dados com base no id fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<EstoqueData> ObterPorId(int id) => await _repo.GetById(id);

        /// <summary>
        /// Criar um novo produto no estoque - Chama o método Add
        /// do repository para adicionar um novo produto ao banco de dados
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>

        public async Task Criar(EstoqueData produto) => await _repo.Add(produto);

        /// <summary>
        /// Atualizar um produto do estoque existente e chama
        /// o método Update do repository
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task Atualizar(EstoqueData produto) => await _repo.Update(produto);

        /// <summary>
        /// Deletar um produto do estoque por id, chamando
        /// o método Delete do repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Deletar(int id) => await _repo.Delete(id);
    }
}
