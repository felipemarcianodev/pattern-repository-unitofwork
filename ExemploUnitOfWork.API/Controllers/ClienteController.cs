using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExemploUnitOfWork.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
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

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> BuscarPorNome(string nome)
        {
            var clientes = await _clienteService.ObterPorNomeAsync(nome);
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Create(Cliente cliente)
        {
            try
            {
                var novoCliente = await _clienteService.AdicionarAsync(cliente);
                return CreatedAtAction(nameof(GetById), new { id = novoCliente.Id }, novoCliente);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _clienteService.RemoverAsync(id);

            if (!sucesso)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            var clientes = await _clienteService.GetTodosAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest("ID do cliente não confere.");
            }

            try
            {
                await _clienteService.AtualizarAsync(cliente);
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