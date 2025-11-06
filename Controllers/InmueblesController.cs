using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria_.Net_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace apiInmobiliariaLabIII
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InmueblesController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public InmueblesController(DataContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        [HttpGet]
        public async Task<IActionResult> getInmuebles()
        {
            try
            {
                string emailToken = User.Identity?.Name;
                if (emailToken == null)
                {
                    return Unauthorized("Usuario no autorizado");
                }

                var propietario = context.Propietario.FirstOrDefault(x => x.email == emailToken);
                if (propietario == null)
                {
                    return NotFound("Propietario no encontrado");
                }
                var inmuebles = await context.Inmuebles.Where(x => x.duenio.idPropietario == propietario.idPropietario).ToListAsync();
                return Ok(inmuebles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("actualizarEstado")]
        public async Task<IActionResult> actualizarEstado([FromBody] Inmueble inmueble)
        {
            try
            {
                string emailToken = User.Identity?.Name;//verificar el usuario logeado edl token
                if (emailToken == null)
                {
                    return Unauthorized("Usuario no autorizado");
                }
                var propietario = context.Propietario.FirstOrDefault(x => x.email == emailToken);//verificar el propietario
                if (propietario == null)
                {
                    return NotFound("Propietario no encontrado");
                }
                var inmuebleOriginal = await context.Inmuebles.FirstOrDefaultAsync(x => x.idInmueble == inmueble.idInmueble);//inmueble de la base
                if (inmuebleOriginal == null)
                {
                    return NotFound("Inmueble no encontrado");
                }
                if (inmuebleOriginal.duenio.idPropietario != propietario.idPropietario)//verificar que el propietario sea el duenio del inmueble
                {
                    return BadRequest("Inmueble no pertenece al propietario");
                }

                inmuebleOriginal.disponible = inmueble.disponible;//solo seteo el estado
                await context.SaveChangesAsync();
                return Ok("Inmueble actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}