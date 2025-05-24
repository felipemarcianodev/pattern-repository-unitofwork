using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetTodosClientesAsync();
        Task<Cliente?> GetClientePorIdAsync(int id);
        Task<Cliente> CriarClienteAsync(Cliente cliente);
        Task<Cliente> AtualizarClienteAsync(Cliente cliente);
        Task<bool> RemoverClienteAsync(int id);
        Task<IEnumerable<Cliente>> BuscarClientesPorNomeAsync(string nome);
    }
}
