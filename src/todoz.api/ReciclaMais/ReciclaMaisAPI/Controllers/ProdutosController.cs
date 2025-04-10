using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;

namespace ReciclaMaisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _context.ProdutosResiduos.ToListAsync();

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProdutoResiduo model)
        {
            if (model.Pontuacao <= 0)
            {
                return BadRequest(new { message = "A pontução deve ser maior do que zero." });
            }

            _context.ProdutosResiduos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.ProdutosResiduos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProdutoResiduo model)
        {
            if (id != model.Id) return BadRequest();

            var modelDb = await _context.ProdutosResiduos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (modelDb == null) return NotFound();

            _context.ProdutosResiduos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.ProdutosResiduos
                .FindAsync(id);

            if (model == null) NotFound();

            _context.ProdutosResiduos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
