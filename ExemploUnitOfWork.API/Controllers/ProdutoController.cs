using System.Net;
using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Models;
using ExemploUnitOfWork.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExemploUnitOfWork.API.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        #region Private Fields

        private readonly IProdutoService _produtoService;

        #endregion Private Fields

        #region Public Constructors

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public async Task<ActionResult<Produto>> Create(Produto produto)
        {
            try
            {
                await _produtoService.AdicionarAsync(produto);

                return Created();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Public Methods
    }
}