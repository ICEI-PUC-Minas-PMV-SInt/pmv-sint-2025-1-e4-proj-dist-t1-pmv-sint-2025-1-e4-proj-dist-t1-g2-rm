using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReciclaMais.API.Data;
using ReciclaMais.API.Models;
using ReciclaMais.API.Models.Dto;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReciclaMais.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuariosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        
            var usuario = await _context.Usuarios
                .Include(u => u.Municipe)
                .Include(u => u.OrgaoPublico)
                .Include(u => u.Administrador)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound(); 
            }

            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioCreateDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                return BadRequest("Usuário inválido.");

           
            if (!string.IsNullOrEmpty(usuarioDTO.Password))
            {
                usuarioDTO.Password = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Password);
            }
            else
            {
                return BadRequest("Password cannot be empty");
            }

          
            Usuario usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Estado = usuarioDTO.Estado,
                Cidade = usuarioDTO.Cidade,
                Bairro = usuarioDTO.Bairro,
                Rua = usuarioDTO.Rua,
                CEP = usuarioDTO.CEP,
                Numero = usuarioDTO.Numero,
                Complemento = usuarioDTO.Complemento,
                Username = usuarioDTO.Username,
                Password = usuarioDTO.Password, 
                Tipo = usuarioDTO.Tipo,
                OrgaoPublico = usuarioDTO.OrgaoPublico,
                Administrador = usuarioDTO.Administrador,
                Municipe = usuarioDTO.Municipe
            };

           
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

            return NoContent();
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioCreateDTO usuarioDto)
        {
           
            var usuario = await _context.Usuarios.FindAsync(id);

      
            if (usuario == null)
            {
                return NotFound(); 
            }

           
            usuario.Tipo = usuario.Tipo; 

            
            usuario.Nome = usuarioDto.Nome;
            usuario.Estado = usuarioDto.Estado;
            usuario.Cidade = usuarioDto.Cidade;
            usuario.Bairro = usuarioDto.Bairro;
            usuario.Rua = usuarioDto.Rua;
            usuario.CEP = usuarioDto.CEP;
            usuario.Numero = usuarioDto.Numero;
            usuario.Complemento = usuarioDto.Complemento;
            usuario.Username = usuarioDto.Username;

            
            if (!string.IsNullOrEmpty(usuarioDto.Password))
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password);
            }

            
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(); 
            }

            _context.Usuarios.Remove(usuario); 
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDTO authenticateDto)
        {
            if (string.IsNullOrWhiteSpace(authenticateDto.Username) || string.IsNullOrWhiteSpace(authenticateDto.Password))
                return BadRequest("Username and password are required.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == authenticateDto.Username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(authenticateDto.Password, usuario.Password))
                return Unauthorized("Invalid credentials.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ReciclaMaisSuperSecureKey123456!@#$");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new { token = jwtToken });
        }
    }
}
