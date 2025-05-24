using ExemploUnitOfWork.API.Interfaces;
using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            if (await _unitOfWork.Produtos.NomeExisteAsync(produto.Nome))
            {
                throw new InvalidOperationException("Já existe um produto com este nome.");
            }

            await _unitOfWork.Produtos.AddAsync(produto);
            await _unitOfWork.SaveChangesAsync();

            return produto;
        }

        public async Task ComprarAsync(int produtoId, decimal quantidade)
        {
            var produto = await _unitOfWork.Produtos.GetByIdAsync(produtoId);
            if(produto is null)
                throw new ArgumentException("Informe o produto");

            produto.Comprar(quantidade);

            _unitOfWork.Produtos.Update(produto);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task VenderAsync(int produtoId, decimal quantidade)
        {
            var produto = await _unitOfWork.Produtos.GetByIdAsync(produtoId);
            if (produto is null)
                throw new ArgumentException("Informe o produto");

            produto.Vender(quantidade);

            _unitOfWork.Produtos.Update(produto);
            await _unitOfWork.SaveChangesAsync();
        }
        
    }
}
