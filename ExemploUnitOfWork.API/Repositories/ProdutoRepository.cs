using ExemploUnitOfWork.API.Context;
using ExemploUnitOfWork.API.Interfaces.Repositories;
using ExemploUnitOfWork.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExemploUnitOfWork.API.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        #region Public Constructors

        public ProdutoRepository(ApplicationDbContext context) : base(context)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> NomeExisteAsync(string nome)
        {
            return await _dbSet.AnyAsync(c => c.Nome.ToUpper() == nome.ToUpper());
        }

        #endregion Public Methods
    }
}