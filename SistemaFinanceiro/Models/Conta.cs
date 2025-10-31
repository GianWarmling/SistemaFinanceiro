using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaFinanceiro.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [JsonIgnore]
        public List<Transacao>? Transacoes { get; set; }
    }
}
