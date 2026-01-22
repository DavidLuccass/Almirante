using Almirante.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Almirante.Infrastructure.Context;
using Almirante.API;

namespace Almirante.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] Usuario novoUsuario)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == novoUsuario.Email);
            if (existe) return BadRequest("Este e-mail já está cadastrado.");

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Usuário cadastrado com sucesso!"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

            if (user == null)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            return Ok(new { mensagem = "Login realizado com sucesso!", nome = user.Nome });
        }
    }
}

