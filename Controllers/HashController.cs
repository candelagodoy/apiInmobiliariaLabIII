

using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net_Core.Models;
using apiInmobiliariaLabIII;


namespace apiInmobiliariaLabIII
{
    [ApiController]
    [Route("api/[controller]")]
    public class HashController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public HashController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet("actualizar-claves")]
        public async Task<IActionResult> ActualizarClaves()
        {
            var salt = configuration["Salt"];
            var propietarios = context.Propietario.ToList();

            foreach (var p in propietarios)
            {
                // Si la clave parece no estar hasheada (no tiene "=" ni longitud larga)
                if (p.clave.Length < 20)
                {
                    p.clave = HashHelper.Hashear(p.clave, salt);
                }
            }

            await context.SaveChangesAsync();
            return Ok("Claves actualizadas correctamente.");
        }
    }

}
