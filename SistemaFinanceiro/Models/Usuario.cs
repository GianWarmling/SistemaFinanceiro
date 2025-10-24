namespace SistemaFinanceiro.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Conta> Contas { get; set; } = new List<Conta>();
    }
}
