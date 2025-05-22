using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;

namespace ReciclaMais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaleConoscoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FaleConoscoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FaleConosco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaleConosco>>> GetFaleConoscos()
        {
            return await _context.FaleConoscos.ToListAsync();
        }

        // GET: api/FaleConosco
        [HttpGet("{id}")]
        public async Task<ActionResult<FaleConosco>> GetFaleConosco(int id)
        {
            var faleConosco = await _context.FaleConoscos.FindAsync(id);

            if (faleConosco == null)
            {
                return NotFound();
            }

            return faleConosco;
        }

        // PUT: api/FaleConosco/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaleConosco(int id, FaleConosco faleConosco)
        {
            if (id != faleConosco.Id)
            {
                return BadRequest();
            }

            _context.Entry(faleConosco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaleConoscoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FaleConosco
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FaleConosco>> PostFaleConosco(FaleConosco faleConosco)
        {
            _context.FaleConoscos.Add(faleConosco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaleConosco", new { id = faleConosco.Id }, faleConosco);
        }

        // DELETE: api/FaleConosco/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaleConosco(int id)
        {
            var faleConosco = await _context.FaleConoscos.FindAsync(id);
            if (faleConosco == null)
            {
                return NotFound();
            }

            _context.FaleConoscos.Remove(faleConosco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FaleConoscoExists(int id)
        {
            return _context.FaleConoscos.Any(e => e.Id == id);
        }
    }
}
