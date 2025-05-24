using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Interfaces.Repositories
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        #region Public Methods

        Task<bool> NomeExisteAsync(string nome);

        #endregion Public Methods
    }
}