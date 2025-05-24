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

        public async Task<Cliente> AdicionarAsync(Cliente cliente)
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

        public async Task<Cliente> AtualizarAsync(Cliente cliente)
        {
            var clienteExistente = await _unitOfWork.Clientes.GetByIdAsync(cliente.Id);
            if (clienteExistente is null)
            {
                throw new ArgumentException("Cliente não encontrado.");
            }

            // Verificar se o email já existe para outro cliente
            var clienteComEmail = await _unitOfWork.Clientes.GetByEmailAsync(cliente.Email);
            if (clienteComEmail is not null && clienteComEmail.Id != cliente.Id)
            {
                throw new InvalidOperationException("Já existe outro cliente com este email.");
            }

            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveChangesAsync();

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetTodosAsync()
        {
            return await _unitOfWork.Clientes.GetAllAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _unitOfWork.Clientes.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObterPorNomeAsync(string nome)
        {
            return await _unitOfWork.Clientes.GetClientesPorNomeAsync(nome);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente is null)
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