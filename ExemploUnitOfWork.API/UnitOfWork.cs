using ExemploUnitOfWork.API.Context;
using ExemploUnitOfWork.API.Interfaces.Repositories;
using ExemploUnitOfWork.API.Interfaces;
using ExemploUnitOfWork.API.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExemploUnitOfWork.API
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        #endregion Private Fields

        #region Public Constructors

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Clientes = new ClienteRepository(_context);
            Produtos = new ProdutoRepository(_context);
            Vendas = new VendaRepository(_context);
        }

        #endregion Public Constructors

        #region Public Properties

        public IClienteRepository Clientes { get; }
        public IProdutoRepository Produtos { get; }
        public IVendaRepository Vendas { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Inicia uma transação no banco de dados
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Confirma a transação atual
        /// </summary>
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        /// <summary>
        /// Desfaz a transação atual
        /// </summary>
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <summary>
        /// Salva todas as alterações no banco de dados
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}