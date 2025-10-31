using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaFinanceiro.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        [Required]
        public int ContaId { get; set; }

        [ForeignKey("ContaId")]
        [ValidateNever]
        public Conta Conta { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; }

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; }
    }
}
