using System.ComponentModel.DataAnnotations;

namespace ExemploUnitOfWork.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter formato válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Celular é obrigatório")]
        [Phone(ErrorMessage = "Celular deve ter formato válido")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "Endereço é obrigatório")]
        [StringLength(200, ErrorMessage = "Endereço deve ter no máximo 200 caracteres")]
        public string Endereco { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
