
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using apiInmobiliariaLabIII.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace apiInmobiliariaLabIII
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PropietariosController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public PropietariosController(DataContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginV loginV)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: loginV.Clave,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"] ?? ""),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));

                var p = await context.Propietario.FirstOrDefaultAsync(x => x.email == loginV.Usuario);
                if (p == null || p.clave != hashed)
                {
                    return BadRequest("Nombre de usuario o clave incorrecta");
                }
                else
                {
                    var secreto = configuration["TokenAuthentication:SecretKey"];
                    if (string.IsNullOrEmpty(secreto))
                        throw new Exception("Falta configurar TokenAuthentication:Secret");
                    var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secreto));
                    var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, p.email),
                        new Claim("FullName", p.nombre + " " + p.apellido),
                        new Claim(ClaimTypes.Role, "Propietario"),
                    };

                    var token = new JwtSecurityToken(
                        issuer: configuration["TokenAuthentication:Issuer"],
                        audience: configuration["TokenAuthentication:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credenciales
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("perfil")]
        [Authorize]
        public async Task<ActionResult<Propietario>> Get()
        {
            try
            {
                string emailToken = User.Identity?.Name;
                if (emailToken == null)
                {
                    return Unauthorized("Usuario no autorizado");
                }

                var usuario = await context.Propietario.SingleOrDefaultAsync(x => x.email == emailToken);
                if (usuario == null)
                {
                    return NotFound("Propietario no encontrado");
                }
                
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("actualizar")]
        [Authorize]
        public async Task<IActionResult> Actualizar([FromBody] Propietario propietario)
        {
            try
            {
                string emailToken = User.Identity?.Name;//email del usuario autenticado
                if (emailToken == null)
                {
                    return Unauthorized("Usuario no autorizado");
                }

                var propietarioO = await context.Propietario.FirstOrDefaultAsync(x => x.email == emailToken);//busca el propietario en la base

                if (propietarioO == null)
                {
                    return NotFound("Propietario no encontrado");
                }

                if (string.IsNullOrEmpty(propietario.clave))
                {
                    propietario.clave = propietarioO.clave;//mantiene la misma clave
                }
                else
                {
                    propietario.clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: propietario.clave,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"] ?? ""),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));//hashea la nueva clave
                }

                propietario.idPropietario = propietarioO.idPropietario;//conserva el mismo id del prop original
                context.Entry(propietarioO).CurrentValues.SetValues(propietario);//copia los valores del request al propietario original
                await context.SaveChangesAsync();
                return Ok("Datos actualizados correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar los datos: {ex.Message}");
            }
        }

        [HttpPut("actualizarClave")]
        [Authorize]
        public async Task<IActionResult> ActualizarClave([FromForm] string _claveAntigua, [FromForm] string _claveNueva)
        {
            try
            {
                string emailToken = User.Identity?.Name;
                if (emailToken == null)
                {
                    return Unauthorized("Usuario no autorizado");
                }

                var propietario = await context.Propietario.FirstOrDefaultAsync(x => x.email == emailToken);
                if (propietario == null)
                {
                    return NotFound("Propietario no encontrado");
                }

                string claveAntigua = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: _claveAntigua,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"] ?? ""),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                if (propietario.clave != claveAntigua)
                {
                    return BadRequest("Clave antigua incorrecta");
                }
                string claveNueva = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: _claveNueva,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"] ?? ""),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));

                propietario.clave = claveNueva;
                await context.SaveChangesAsync();
                return Ok("Clave actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}