using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetTodosAsync();
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<Cliente> AdicionarAsync(Cliente cliente);
        Task<Cliente> AtualizarAsync(Cliente cliente);
        Task<bool> RemoverAsync(int id);
        Task<IEnumerable<Cliente>> ObterPorNomeAsync(string nome);
    }
}
