using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using ReciclaMais.API.Models.Dto;

namespace ReciclaMais.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaleConoscoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FaleConoscoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FaleConosco
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var mensagens = await _context.FaleConoscos.ToListAsync();
            return Ok(mensagens);
        }

        // GET: api/FaleConosco/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var mensagem = await _context.FaleConoscos.FindAsync(id);
            if (mensagem == null) return NotFound();

            return Ok(mensagem);
        }

        // POST: api/FaleConosco
        [HttpPost]
        public async Task<ActionResult<FaleConosco>> Create(FaleConoscoCreateDTO dto)
        {
            var novaMensagem = new FaleConosco
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email
            };

            _context.FaleConoscos.Add(novaMensagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = novaMensagem.Id }, novaMensagem);
        }

        // PUT: api/FaleConosco/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FaleConoscoCreateDTO dto)
        {
            var faleconoscoExistente = await _context.FaleConoscos.FindAsync(id);

            if (faleconoscoExistente == null)
            {
                return NotFound();
            }

            faleconoscoExistente.Nome = dto.Nome;
            faleconoscoExistente.Telefone = dto.Telefone;
            faleconoscoExistente.Email = dto.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FaleConosco/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mensagem = await _context.FaleConoscos.FindAsync(id);
            if (mensagem == null) return NotFound();

            _context.FaleConoscos.Remove(mensagem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/FaleConosco/novo-contato
        [HttpPost("novo-contato")]
        public async Task<ActionResult<FaleConosco>> CreateNovoContato(FaleConoscoCreateDTO dto)
        {
            var novoContato = new FaleConosco
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Email = dto.Email
            };

            _context.FaleConoscos.Add(novoContato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = novoContato.Id }, novoContato);
        }
    }
}
