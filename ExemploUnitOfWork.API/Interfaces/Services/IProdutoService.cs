using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Services
{
    public interface IProdutoService
    {
        #region Public Methods

        Task<Produto> AdicionarAsync(Produto produto);

        Task ComprarAsync(int produtoId, decimal quantidade);

        Task VenderAsync(int produtoId, decimal quantidade);

        #endregion Public Methods
    }
}