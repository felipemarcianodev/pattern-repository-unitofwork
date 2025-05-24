using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Repositories
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<Cliente?> GetByEmailAsync(string email);
        Task<IEnumerable<Cliente>> GetClientesPorNomeAsync(string nome);
        Task<bool> EmailExisteAsync(string email);
    }
}
