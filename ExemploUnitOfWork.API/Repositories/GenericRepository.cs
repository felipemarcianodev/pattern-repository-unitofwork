using ExemploUnitOfWork.API.Context;
using ExemploUnitOfWork.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExemploUnitOfWork.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Protected Fields

        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        #endregion Protected Fields

        #region Public Constructors

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        #endregion Public Methods
    }
}