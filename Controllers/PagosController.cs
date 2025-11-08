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
    public class PagosController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public PagosController(DataContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        [HttpGet("contrato/{idContrato}")]
        public async Task<IActionResult> obtenerPagosPorContrato(int idContrato)
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

                var contrato = await context.Contratos //busco contratos y verifico q pertenezca a un inmueble del propietario
                    .Include(c => c.inmueble)
                    .ThenInclude(i => i.duenio)
                    .FirstOrDefaultAsync(x => x.idContrato == idContrato);

                if (contrato == null || contrato.inmueble.duenio.idPropietario != propietario.idPropietario)
                {
                    return BadRequest("Contrato no encontrado o no pertenece al propietario");
                }

                var pagos = await context.Pagos // busco pagos asociados al contrato
                    .Where(p => p.idContrato == idContrato)
                    .ToListAsync();

                if (pagos == null || pagos.Count == 0)
                {
                    return Ok("No hay pagos registrados para este contrato.");
                }
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}