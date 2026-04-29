using ApiVendas.Data;
using ApiVendas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiVendas.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        /// <summary>
        /// AppDbContext - contexto do banco de dados - responsável por
        /// gerenciar a conexão com o banco de dados e fornecer acesso
        /// is tabelas e entidades do banco de dados
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor da classe - recebeu o contexto do banco de dados 
        /// </summary>
        /// <param name="context"></param>"
        public EstoqueRepository(AppDbContext context) 
        { 
            _context = context;
        }

        /// <summary>
        /// O metódo GetAll é responsável 
        /// por retornar uma lista de todos os produtos no estoque do banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<List<EstoqueData>> GetAll() =>
            await _context.Estoque.ToListAsync();

        /// <summary>
        /// GetById é responsável por retornar um produto específico pelo seu ID do banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EstoqueData> GetById(int id) => await _context.Estoque.FindAsync(id);


        /// <summary>
        /// Add é responsável por adicionar um novo 
        /// produto no estoque do banco de dados
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task Add(EstoqueData produto) 
        {
            _context.Estoque.Add(produto);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update é responsável por atualizar um produto 
        /// existente no banco de dados
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task Update(EstoqueData produto)
        {
            try
            {
                var produto_existe = await _context.Estoque.FindAsync(produto.Id);

                //Coloca um aviso genérico caso o produto não seja encontrado
                if (produto_existe == null)
                    throw new Exception("Nenhum produto foi encontrado no estoque.");

                //Atualiza os valores do objeto existente com os novo objeto
                _context.Entry(produto_existe).CurrentValues.SetValues(produto);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno no banco de dados.");
            }
        }

        /// <summary>
        /// Delete é responsável por excluir um produto
        /// do banco de dados pelo seu ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            try
            {

                var produto_existe = await GetById(id);

                //Coloca um aviso genérico caso o produto não seja encontrado
                if (produto_existe == null)
                    throw new Exception("Nenhum produto foi encontrado no estoque.");

                _context.Estoque.Remove(p);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception("Ocorreu um erro interno no banco de dados.");
            }
        }

        

        

        
    }
}
