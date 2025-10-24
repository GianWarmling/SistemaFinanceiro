namespace SistemaFinanceiro.Models
{
    public enum TipoTransacao { Credito, Debito }

    public class Transacao
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; } = null!;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.Now;
    }
}
