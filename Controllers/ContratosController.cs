
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
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
    [Authorize]
    public class ContratosController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public ContratosController(DataContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        [HttpGet("porInmueble/{idInmueble}")]
        public async Task<IActionResult> obtenerContratosPorInmueble(int idInmueble)
        {
            try
            {
                string emailToken = User.Identity?.Name;//verificar el usuario logeado del token
                if (emailToken == null)
                {
                    return Unauthorized("Usuario no autorizado");
                }

                var propietario = await context.Propietario.FirstOrDefaultAsync(x => x.email == emailToken);
                if (propietario == null)
                {
                    return NotFound("Propietario no encontrado");
                }

                var inmueble = await context.Inmuebles
                    .Include(x => x.duenio)
                    .FirstOrDefaultAsync(x => x.idInmueble == idInmueble);//verificando q el inmueble exista y pertenezca al propietario

                if (inmueble == null || inmueble.duenio.idPropietario != propietario.idPropietario)
                {
                    return BadRequest("Inmueble no encontrado o no pertenece al propietario");
                }

                var contratos = await context.Contratos//busca los contratos asociados a ese inmueble
                    .Include(c => c.inmueble)
                    .Include(c => c.inquilino)
                    .Where(x => x.inmueble.idInmueble == idInmueble)
                    .ToListAsync();

                if (contratos == null || contratos.Count == 0)
                {
                    return Ok("No hay contratos asociados a este inmueble.");
                }
                return Ok(contratos);
            }
            catch
            {
                return BadRequest("Error al obtener los contratos.");
            }
        }
    }


}
