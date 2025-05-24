using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Services
{
    public interface IProdutoService
    {
        #region Public Methods

        Task<Produto> AdicionarAsync(Produto produto);

        Task ComprarAsync(Produto produto, decimal quantidade);

        Task VenderAsync(Produto produto, decimal quantidade);

        #endregion Public Methods
    }
}