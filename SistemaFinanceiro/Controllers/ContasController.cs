using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Data;
using SistemaFinanceiro.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<ContasController>
        [HttpGet]
        public async Task<IActionResult> GetContas()
        {
            var contas = await _context.Contas.Include(c => c.Usuario).ToListAsync();
            return Ok(contas);
        }

        // GET api/<ContasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConta(int id)
        {
            var conta = await _context.Contas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);

            if (conta == null)
            {
                return NotFound("Conta não encontrada!");
            }
            return Ok(conta);
        }

        // POST api/<ContasController>
        [HttpPost]
        public async Task<IActionResult> CriarConta([FromBody] Conta conta)
        {
            var usuario = await _context.Usuarios.FindAsync(conta.UsuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado!");
            }
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConta), new {id =  conta.Id}, conta);
        }

        // PUT api/<ContasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConta(int id, [FromBody] Conta conta)
        {
            var contaDb = await _context.Contas.FindAsync(id);
            if (contaDb == null)
            {
                return NotFound("Conta não encontrada!");
            }
            contaDb.Nome = conta.Nome;
            contaDb.Saldo = conta.Saldo;
            await _context.SaveChangesAsync();
            return Ok(conta);
        }

        // DELETE api/<ContasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarConta(int id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta == null)
            {
                return NotFound("Conta não encontrada!");
            }
            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
