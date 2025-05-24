namespace ExemploUnitOfWork.API.Models
{
    public class Produto
    {
        public Produto()
        {
            DataCadastro = DateTime.Now;
        }
        public int Id { get; set; }
        public decimal Valor { get; set; } 
        public DateTime DataCadastro { get; }
    }
}
