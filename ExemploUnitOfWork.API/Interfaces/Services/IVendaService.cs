using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Services
{
    public interface IVendaService
    {
        Task AdicionarAsync(Venda venda);
    }
}
