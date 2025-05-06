using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using ReciclaMais.API.Models.Dto;
using System.Text.Json.Serialization;

namespace ReciclaMais.API.Controllers
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

            if (dto.Data <= DateTime.Today)
                return BadRequest("A data do agendamento deve ser após a data atual.");

            foreach (var item in dto.ItensColeta)
            {
                if (item.Quantidade <= 0)
                    return BadRequest($"A quantidade do produto {item.ProdutoId} deve ser maior que zero.");

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

            agendamentoExistente.Data = dto.Data;
            agendamentoExistente.Hora = dto.Hora;

            int pontuacaoTotal = 0;
            var novaListaItens = new List<ItemColeta>();

            foreach (var itemDto in dto.ItensColeta)
            {
                if (itemDto.Quantidade <= 0)
                    return BadRequest($"A quantidade do produto {itemDto.ProdutoId} deve ser maior que zero.");

                var produto = await _context.ProdutosResiduos.FindAsync(itemDto.ProdutoId);
                if (produto == null)
                    return BadRequest($"Produto com ID {itemDto.ProdutoId} não encontrado.");

                ItemColeta item;

                if (itemDto.Id.HasValue && itemDto.Id.Value > 0)
                {
                    item = agendamentoExistente.ItensColeta.FirstOrDefault(i => i.Id == itemDto.Id.Value);

                    if (item != null)
                    {
                        item.ProdutoId = itemDto.ProdutoId;
                        item.Produto = produto;
                        item.Quantidade = itemDto.Quantidade;
                        item.Estado = (Models.Enum.EstadoConservacao)itemDto.Estado;
                    }
                    else
                    {
                        item = new ItemColeta
                        {
                            ProdutoId = itemDto.ProdutoId,
                            Produto = produto,
                            Quantidade = itemDto.Quantidade,
                            Estado = (Models.Enum.EstadoConservacao)itemDto.Estado
                        };
                    }
                }
                else
                {
                    item = new ItemColeta
                    {
                        ProdutoId = itemDto.ProdutoId,
                        Produto = produto,
                        Quantidade = itemDto.Quantidade,
                        Estado = (Models.Enum.EstadoConservacao)itemDto.Estado
                    };
                }

                pontuacaoTotal += (produto.Pontuacao * item.Quantidade * (int)item.Estado) / 100;
                novaListaItens.Add(item);
            }

            // Remove os itens que não estão mais presentes.
            _context.ItensColeta.RemoveRange(agendamentoExistente.ItensColeta
                .Where(i => !novaListaItens.Any(n => n.Id == i.Id)));

            agendamentoExistente.ItensColeta = novaListaItens;
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
