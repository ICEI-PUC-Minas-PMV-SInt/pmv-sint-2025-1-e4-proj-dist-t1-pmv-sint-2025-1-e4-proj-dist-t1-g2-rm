using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Data;

namespace ReciclaMaisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemColetaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemColetaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var itens = await _context.ItensColeta
                .Include(i => i.Produto)
                .Include(i => i.Agendamento)
                .ToListAsync();

            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.ItensColeta
                .Include(i => i.Produto)
                .Include(i => i.Agendamento)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST, PUT, DELETE foram removidos. São desnecessários.
    }
}
