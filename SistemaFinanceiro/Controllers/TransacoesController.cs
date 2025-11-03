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
            var conta = await _context.Contas.FindAsync(transacao.ContaId);
            if (conta == null)
            {
                return NotFound(new { mensagem = "Conta não encontrada" });
            }

            if (transacao.Tipo == TipoTransacao.Debito && conta.Saldo < transacao.Valor)
            {
                return BadRequest(new { mensagem = "Saldo insuficiente" });
            }

            if (transacao.Tipo == TipoTransacao.Credito)
            {
                conta.Saldo += transacao.Valor;
            }
            else
            {
                conta.Saldo -= transacao.Valor;
            }

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Transação registrada com sucesso!", saldoAtual = conta.Saldo });
        }

        [HttpGet("conta/{contaId}")]
        public async Task<IActionResult> ListarTransacoes(int contaId)
        {
            var conta = await _context.Contas.FindAsync(contaId);

            if (conta == null)
            {
                return NotFound(new { mensagem = "Conta não encontrada!" });
            }

            var transacoes = await _context.Transacoes
                .Where(t => t.ContaId == contaId)
                .OrderByDescending(t => t.Data)
                .ToListAsync();

            return Ok(new
            {
                contaId = conta.Id,
                nomeConta = conta.Nome,
                saldoAtual = conta.Saldo,
                transacoes
            });

        }
    }
}
