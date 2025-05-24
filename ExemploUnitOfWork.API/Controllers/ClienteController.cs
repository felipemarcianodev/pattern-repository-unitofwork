using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExemploUnitOfWork.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        #region Private Fields

        private readonly IClienteService _clienteService;

        #endregion Private Fields

        #region Public Constructors

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Busca clientes por nome
        /// </summary>
        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> BuscarPorNome(string nome)
        {
            var clientes = await _clienteService.BuscarClientesPorNomeAsync(nome);
            return Ok(clientes);
        }

        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            try
            {
                var novoCliente = await _clienteService.CriarClienteAsync(cliente);
                return CreatedAtAction(nameof(GetCliente), new { id = novoCliente.Id }, novoCliente);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um cliente
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var sucesso = await _clienteService.RemoverClienteAsync(id);

            if (!sucesso)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Obtém um cliente específico pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClientePorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        /// <summary>
        /// Obtém todos os clientes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetTodosClientesAsync();
            return Ok(clientes);
        }

        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest("ID do cliente não confere.");
            }

            try
            {
                await _clienteService.AtualizarClienteAsync(cliente);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Public Methods
    }
}