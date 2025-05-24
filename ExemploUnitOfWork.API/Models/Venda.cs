using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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


        [Required(ErrorMessage = "A quantidade é obrigatória")]
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; } = DateTime.Now;


        [ValidateNever]
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public virtual Produto Produto { get; set; }
    }
}
