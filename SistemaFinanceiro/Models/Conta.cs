namespace SistemaFinanceiro.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
