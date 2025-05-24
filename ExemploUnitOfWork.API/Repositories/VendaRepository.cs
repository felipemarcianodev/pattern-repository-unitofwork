using ExemploUnitOfWork.API.Context;
using ExemploUnitOfWork.API.Interfaces.Repositories;
using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Repositories
{
    public class VendaRepository : GenericRepository<Venda>, IVendaRepository
    {
        #region Public Constructors

        public VendaRepository(ApplicationDbContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}