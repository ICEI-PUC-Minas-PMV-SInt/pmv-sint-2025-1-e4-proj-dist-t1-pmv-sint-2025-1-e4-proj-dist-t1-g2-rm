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

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var mensagens = await _context.FaleConoscos.ToListAsync();
            return Ok(mensagens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var mensagem = await _context.FaleConoscos.FindAsync(id);
            if (mensagem == null) return NotFound();

            return Ok(mensagem);
        }

        [HttpPost]
        public async Task<ActionResult<FaleConosco>> Create(FaleConoscoCreateDTO dto)
        {
            var novaMensagem = new FaleConosco
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone
            };

            _context.FaleConoscos.Add(novaMensagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = novaMensagem.Id }, novaMensagem);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mensagem = await _context.FaleConoscos.FindAsync(id);
            if (mensagem == null) return NotFound();

            _context.FaleConoscos.Remove(mensagem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<bool> Create(FaleConosco novoContato)
        {
            return CreatedAtAction(nameof(GetById), new { id = novaMensagem.Id }, novaMensagem);

        }
    }
}
