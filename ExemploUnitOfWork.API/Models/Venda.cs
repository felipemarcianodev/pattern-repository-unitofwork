using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExemploUnitOfWork.API.Models
{
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O produto é obrigatório")]
        public int ProdutoId { get; set; }
        public DateTime DataCadastro { get; } = DateTime.Now;

        public virtual Cliente Cliente { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
