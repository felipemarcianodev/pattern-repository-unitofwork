using ExemploUnitOfWork.API.Interfaces;
using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Services
{
    public class VendaService : IVendaService
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public VendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task AdicionarAsync(Venda venda)
        {
            if (!await _unitOfWork.Produtos.ExistsAsync(venda.ProdutoId))
            {
                throw new InvalidOperationException("Informe o produto.");
            }

            if (!await _unitOfWork.Clientes.ExistsAsync(venda.ClienteId))
            {
                throw new InvalidOperationException("Informe o cliente.");
            }

            await _unitOfWork.Vendas.AddAsync(venda);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}