using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReciclaMais.API.Models;
using System.Collections.Generic;
using ReciclaMais.API.Models.Dto;
using ReciclaMais.API.Data;

namespace ReciclaMais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // ainda não foram implementados os roles
    public class BeneficiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BeneficiosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Beneficios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeneficioResponseDTO>>> GetAll()
        {
            var beneficios = await _context.Beneficios
                .Select(b => new BeneficioResponseDTO
                {
                    Id = b.Id,
                    Titulo = b.Titulo,
                    Descricao = b.Descricao,
                    PontosNecessarios = b.PontosNecessarios
                })
                .ToListAsync();

            return Ok(beneficios);
        }

        // GET: api/Beneficios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BeneficioResponseDTO>> GetById(int id)
        {
            var beneficio = await _context.Beneficios.FindAsync(id);

            if (beneficio == null)
                return NotFound();

            var dto = new BeneficioResponseDTO
            {
                Id = beneficio.Id,
                Titulo = beneficio.Titulo,
                Descricao = beneficio.Descricao,
                PontosNecessarios = beneficio.PontosNecessarios
            };

            return Ok(dto);
        }

        // POST: api/Beneficios
        [HttpPost]
        public async Task<ActionResult> Create(BeneficioCreateDTO dto)
        {
            try
            {
                if (dto.PontosNecessarios <= 0)
                {
                    return BadRequest(new { message = "Os pontos necessários devem ser maiores que zero." });
                }

                if (string.IsNullOrWhiteSpace(dto.Titulo) || string.IsNullOrWhiteSpace(dto.Descricao))
                {
                    return BadRequest(new { message = "Título e Descrição são obrigatórios." });
                }

                var beneficio = new Beneficio
                {
                    Titulo = dto.Titulo,
                    Descricao = dto.Descricao,
                    PontosNecessarios = dto.PontosNecessarios
                };

                _context.Beneficios.Add(beneficio);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = beneficio.Id }, beneficio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro no servidor: " + ex.Message });
            }
        }

        // PUT: api/Beneficios/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeneficioCreateDTO dto)
        {
            // Busca a entidade existente no banco de dados
            var beneficioExistente = await _context.Beneficios.FindAsync(id);

            if (beneficioExistente == null)
            {
                return NotFound(); 
            }

            // Atualiza os campos da entidade existente
            beneficioExistente.Titulo = dto.Titulo;
            beneficioExistente.Descricao = dto.Descricao;
            beneficioExistente.PontosNecessarios = dto.PontosNecessarios;

            
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        // DELETE: api/Beneficios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beneficio = await _context.Beneficios.FindAsync(id);
            if (beneficio == null)
                return NotFound();

            _context.Beneficios.Remove(beneficio);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Benefício deletado com sucesso"});
        }
    }
}
