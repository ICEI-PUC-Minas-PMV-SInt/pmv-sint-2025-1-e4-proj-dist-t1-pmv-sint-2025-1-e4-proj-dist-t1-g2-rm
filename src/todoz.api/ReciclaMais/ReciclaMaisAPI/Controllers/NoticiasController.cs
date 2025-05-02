using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;

namespace ReciclaMaisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NoticiasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var noticias = await _context.Noticias.ToListAsync();
            return Ok(noticias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null) return NotFound();
            return Ok(noticia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Noticia model)
        {
            if (string.IsNullOrWhiteSpace(model.Titulo) || string.IsNullOrWhiteSpace(model.Conteudo))
            {
                return BadRequest(new { message = "Título e conteúdo são obrigatórios." });
            }

            model.DataPublicacao = DateTime.UtcNow;

            _context.Noticias.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Noticia model)
        {
            if (id != model.Id) return BadRequest();

            var noticiaDb = await _context.Noticias.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            if (noticiaDb == null) return NotFound();

            _context.Noticias.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null) return NotFound();

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
