using apiInmobiliariaLabIII.Models;
using apiInmobiliariaLabIIII.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiInmobiliariaLabIII
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Propietario> Propietario { get; set; }
		public DbSet<Inmueble> Inmuebles { get; set; }
		public DbSet<Inquilino> Inquilinos { get; set; }
		public DbSet<Contrato> Contratos { get; set; }
		public DbSet<Pago> Pagos { get; set; }
	}
}