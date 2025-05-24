using ExemploUnitOfWork.API.Context;
using ExemploUnitOfWork.API.Interfaces.Repositories;
using ExemploUnitOfWork.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExemploUnitOfWork.API.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        #region Public Constructors

        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _dbSet.AnyAsync(c => c.Email == email);
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Cliente>> GetClientesPorNomeAsync(string nome)
        {
            return await _dbSet
                .Where(c => c.Nome.Contains(nome))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        #endregion Public Methods
    }
}