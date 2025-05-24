using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExemploUnitOfWork.API.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O saldo em estoque é obrigatório")]
        public decimal SaldoEstoque { get; set; }
        public DateTime DataCadastro { get; } = DateTime.Now;

        [JsonIgnore]
        public virtual ICollection<Venda> Vendas { get; set; } = new List<Venda>();

        public virtual void Vender(decimal quantidadeDiminuir)
        {
            if (SaldoEstoque <= 0 ||
                SaldoEstoque < quantidadeDiminuir)
                throw new InvalidOperationException("Produto não possui estoque suficiente");
            
            SaldoEstoque -= quantidadeDiminuir;
        }

        public virtual void Comprar(decimal quantidadeAumentar)
        {
            if (SaldoEstoque <= 0)
                throw new InvalidOperationException("Produto não possui estoque suficiente");

            SaldoEstoque += quantidadeAumentar;
        }
    }
}
