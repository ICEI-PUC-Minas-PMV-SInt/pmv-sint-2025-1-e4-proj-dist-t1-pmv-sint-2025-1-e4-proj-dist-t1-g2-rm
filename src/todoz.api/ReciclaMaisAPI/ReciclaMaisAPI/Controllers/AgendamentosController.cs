using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Data;
using ReciclaMaisAPI.Models;
using ReciclaMaisAPI.Models.Dto;
using System.Text.Json.Serialization;

namespace ReciclaMaisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.ItensColeta)
                    .ThenInclude(i => i.Produto)
                .ToListAsync();

            var dtoList = agendamentos.Select(model => new AgendamentoResponseDTO
            {
                Id = model.Id,
                Data = model.Data,
                Hora = model.Hora,
                PontuacaoTotal = model.PontuacaoTotal,
                ItensColeta = model.ItensColeta.Select(item => new ItemColetaResponseDTO
                {
                    Id = item.Id,
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.Produto?.Nome,
                    Quantidade = item.Quantidade,
                    Estado = (int)item.Estado,
                    Pontuacao = (item.Produto.Pontuacao * item.Quantidade * (int)item.Estado) / 100
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AgendamentoCreateDTO dto)
        {
            var agendamento = new Agendamento
            {
                Data = dto.Data,
                Hora = dto.Hora,
                ItensColeta = new List<ItemColeta>()
            };

            int pontuacaoTotal = 0;

            foreach (var item in dto.ItensColeta)
            {
                var produto = await _context.ProdutosResiduos.FindAsync(item.ProdutoId);

                if (produto == null)
                    return BadRequest($"Produto com ID {item.ProdutoId} não encontrado.");

                var novoItem = new ItemColeta
                {
                    ProdutoId = produto.Id,
                    Produto = produto,
                    Quantidade = item.Quantidade,
                    Estado = (Models.Enum.EstadoConservacao)item.Estado
                };

                pontuacaoTotal += (produto.Pontuacao * novoItem.Quantidade * (int)novoItem.Estado) / 100;
                agendamento.ItensColeta.Add(novoItem);
            }

            agendamento.PontuacaoTotal = pontuacaoTotal;

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = agendamento.Id }, agendamento);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Agendamentos
                .Include(a => a.ItensColeta)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (model == null)
                return NotFound();

            var dto = new AgendamentoResponseDTO
            {
                Id = model.Id,
                Data = model.Data,
                Hora = model.Hora,
                PontuacaoTotal = model.PontuacaoTotal,
                ItensColeta = model.ItensColeta.Select(item => new ItemColetaResponseDTO
                {
                    Id = item.Id,
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.Produto?.Nome,
                    Quantidade = item.Quantidade,
                    Estado = (int)item.Estado,
                    Pontuacao = (item.Produto.Pontuacao * item.Quantidade * (int)item.Estado) / 100
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AgendamentoCreateDTO dto)
        {
            var agendamentoExistente = await _context.Agendamentos
                .Include(a => a.ItensColeta)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamentoExistente == null)
                return NotFound();

            _context.ItensColeta.RemoveRange(agendamentoExistente.ItensColeta);

            agendamentoExistente.Data = dto.Data;
            agendamentoExistente.Hora = dto.Hora;
            agendamentoExistente.ItensColeta = new List<ItemColeta>();

            int pontuacaoTotal = 0;

            foreach (var item in dto.ItensColeta)
            {
                var produto = await _context.ProdutosResiduos.FindAsync(item.ProdutoId);

                if (produto == null)
                    return BadRequest($"Produto com ID {item.ProdutoId} não encontrado.");

                var novoItem = new ItemColeta
                {
                    ProdutoId = produto.Id,
                    Produto = produto,
                    Quantidade = item.Quantidade,
                    Estado = (Models.Enum.EstadoConservacao)item.Estado
                };

                pontuacaoTotal += (produto.Pontuacao * novoItem.Quantidade * (int)novoItem.Estado) / 100;
                agendamentoExistente.ItensColeta.Add(novoItem);
            }

            agendamentoExistente.PontuacaoTotal = pontuacaoTotal;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Agendamentos
                .Include(a => a.ItensColeta)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (model == null)
                return NotFound();

            _context.ItensColeta.RemoveRange(model.ItensColeta);
            _context.Agendamentos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
