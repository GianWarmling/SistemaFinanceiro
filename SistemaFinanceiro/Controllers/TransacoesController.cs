using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Data;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransacoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarTransacao([FromBody] Transacao transacao)
        {
            // Verifica se a conta existe
            var conta = await _context.Contas.FindAsync(transacao.ContaId);
            if (conta == null)
                return NotFound(new { mensagem = "Conta não encontrada" });

            // Valida saldo para débito
            if (transacao.Tipo == TipoTransacao.Debito && conta.Saldo < transacao.Valor)
                return BadRequest(new { mensagem = "Saldo insuficiente" });

            // Atualiza saldo da conta
            conta.Saldo += transacao.Tipo == TipoTransacao.Credito ? transacao.Valor : -transacao.Valor;

            // Adiciona a transação no banco
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return Ok(new { transacao.Id, novoSaldo = conta.Saldo });
        }

        [HttpGet("conta/{contaId}")]
        public async Task<IActionResult> ListarTransacoes(int contaId)
        {
            var transacoes = await _context.Transacoes
                .Where(t => t.ContaId == contaId)
                .OrderByDescending(t => t.Data)
                .ToListAsync();

            return Ok(transacoes);
        }
    }
}
