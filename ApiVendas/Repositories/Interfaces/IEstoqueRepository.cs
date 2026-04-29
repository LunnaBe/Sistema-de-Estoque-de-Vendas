using Shared;

namespace ApiVendas.Repositories.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<List<EstoqueData>> GetAll();
        Task<EstoqueData> GetById(int id);
        Task Add(EstoqueData produto);
        Task Update(EstoqueData produto);
        Task Delete(int id);
    }
}
