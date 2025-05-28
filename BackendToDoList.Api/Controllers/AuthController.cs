using BackendToDoList.Application.InputModels.Usuario;
using BackendToDoList.Core.Entities;
using BackendToDoList.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly SistemaToDoListDbContext _context;

    public AuthController(IConfiguration configuration, SistemaToDoListDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(CreateUsuarioDto dto)
    {
        if (_context.Usuarios.Any(u => u.Email == dto.Email))
            return BadRequest("Email já registrado");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok(new {status = true, mensagem = "Usuário registrado com sucesso"});
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUsuarioDto dto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email && u.Senha == dto.Senha);
        if (usuario == null)
            return Unauthorized(new {status = false, mensagem = "E-mail ou senha incorretos!"});

        var token = GerarToken(usuario);
        return Ok(new { status = true, token, usuario });
    }

    private string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
