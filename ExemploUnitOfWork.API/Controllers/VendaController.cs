using ExemploUnitOfWork.API.Interfaces;
using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Models;
using ExemploUnitOfWork.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExemploUnitOfWork.API.Controllers
{
    [ApiController]
    [Route("api/vendas")]
    public class VendaController : ControllerBase
    {
        #region Private Fields

        private readonly IClienteService _clienteService;
        private readonly IProdutoService _produtoService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IVendaService _vendaService;

        #endregion Private Fields

        #region Public Constructors

        public VendaController(
            IVendaService vendaService,
            IClienteService clienteService,
            IProdutoService produtoService,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _vendaService = vendaService;
            _clienteService = clienteService;
            _produtoService = produtoService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Venda com transação
        /// </summary>
        /// <param name="venda"></param>
        /// <returns></returns>
        [HttpPost("com-transacao")]
        public async Task<ActionResult<Venda>> CreateComTransacao(Venda venda)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _vendaService.AdicionarAsync(venda);
                await _clienteService.AtualizarDataUltimaCompraAsync(venda.ClienteId);

                //throw new Exception("Testando a transação do Unit of Work");

                await _produtoService.VenderAsync(venda.ProdutoId, venda.Quantidade);
                await _unitOfWork.CommitTransactionAsync();

                return Created();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Venda sem transação
        /// </summary>
        /// <param name="venda"></param>
        /// <returns></returns>
        [HttpPost("sem-transacao")]
        public async Task<ActionResult<Venda>> CreateSemTransacao(Venda venda)
        {
            try
            {
                await _vendaService.AdicionarAsync(venda);
                await _clienteService.AtualizarDataUltimaCompraAsync(venda.Id);

                throw new Exception("Testando a transação do Unit of Work");

                await _produtoService.ComprarAsync(venda.ProdutoId, venda.Quantidade);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Public Methods
    }
}