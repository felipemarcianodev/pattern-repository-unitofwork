using ExemploUnitOfWork.API.Interfaces.Services;
using ExemploUnitOfWork.API.Interfaces;
using ExemploUnitOfWork.API.Models;

namespace ExemploUnitOfWork.API.Services
{
    public class ClienteService : IClienteService
    {
        #region Private Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Atualiza os dados de um cliente existente
        /// </summary>
        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            var clienteExistente = await _unitOfWork.Clientes.GetByIdAsync(cliente.Id);
            if (clienteExistente == null)
            {
                throw new ArgumentException("Cliente não encontrado.");
            }

            // Verificar se o email já existe para outro cliente
            var clienteComEmail = await _unitOfWork.Clientes.GetByEmailAsync(cliente.Email);
            if (clienteComEmail != null && clienteComEmail.Id != cliente.Id)
            {
                throw new InvalidOperationException("Já existe outro cliente com este email.");
            }

            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveChangesAsync();

            return cliente;
        }

        /// <summary>
        /// Busca clientes por nome (busca parcial)
        /// </summary>
        public async Task<IEnumerable<Cliente>> BuscarClientesPorNomeAsync(string nome)
        {
            return await _unitOfWork.Clientes.GetClientesPorNomeAsync(nome);
        }

        /// <summary>
        /// Cria um novo cliente no sistema
        /// </summary>
        public async Task<Cliente> CriarClienteAsync(Cliente cliente)
        {
            // Validação de negócio: Email deve ser único
            if (await _unitOfWork.Clientes.EmailExisteAsync(cliente.Email))
            {
                throw new InvalidOperationException("Já existe um cliente com este email.");
            }

            await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();

            return cliente;
        }

        /// <summary>
        /// Obtém um cliente específico pelo ID
        /// </summary>
        public async Task<Cliente?> GetClientePorIdAsync(int id)
        {
            return await _unitOfWork.Clientes.GetByIdAsync(id);
        }

        /// <summary>
        /// Obtém todos os clientes cadastrados
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetTodosClientesAsync()
        {
            return await _unitOfWork.Clientes.GetAllAsync();
        }

        /// <summary>
        /// Remove um cliente do sistema
        /// </summary>
        public async Task<bool> RemoverClienteAsync(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
            {
                return false;
            }

            _unitOfWork.Clientes.Delete(cliente);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        #endregion Public Methods
    }
}