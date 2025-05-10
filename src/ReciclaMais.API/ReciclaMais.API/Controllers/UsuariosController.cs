using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReciclaMais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Municipe)
                .Include(u => u.OrgaoPublico)
                .Include(u => u.Administrador)
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            // Fetch a single user by ID
            var usuario = await _context.Usuarios
                .Include(u => u.Municipe)
                .Include(u => u.OrgaoPublico)
                .Include(u => u.Administrador)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound(); // Return 404 if not found
            }

            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Usuário inválido.");

            // Set TipoUsuario based on provided details
            if (!string.IsNullOrWhiteSpace(usuario.Municipe?.Cpf))
            {
                usuario.Tipo = TipoUsuario.Municipe;
            }
            else if (!string.IsNullOrWhiteSpace(usuario.Administrador?.NomeAdmin))
            {
                usuario.Tipo = TipoUsuario.Administrador;
            }
            else
            {
                usuario.Tipo = TipoUsuario.OrgaoPublico;
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest(); // Return 400 if ID in URL doesn't match the provided usuario ID
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Try to save the changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound(); // Return 404 if the user doesn't exist
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 for a successful update with no content to return
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(); // Return 404 if the user to delete doesn't exist
            }

            _context.Usuarios.Remove(usuario); // Remove the user
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 for a successful deletion with no content to return
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id); // Check if the user exists in the database
        }
    }
}
