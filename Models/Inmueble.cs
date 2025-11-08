using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace apiInmobiliariaLabIII.Models
{
    [Table("Inmueble")]
    public class Inmueble
    {
        [Key]
        public int? idInmueble { get; set; }
        [MaxLength(50)]
        public string? direccion { get; set; }
        [MaxLength(50)]
        public string? uso { get; set; }
        [MaxLength(50)]
        public string? tipo { get; set; }
        public int? ambientes { get; set; }
        public int? superficie { get; set; }
        public decimal? latitud { get; set; }
        public decimal? valor { get; set; }
        [MaxLength(50)]
        public string? imagen { get; set; }
        public Boolean? disponible { get; set; }
        public decimal? longitud { get; set; }
        public int? idPropietario { get; set; }

        [ForeignKey(nameof(idPropietario))]
        public Propietario? duenio { get; set; }
        public Boolean? tieneContratoVigente { get; set; }
    }

}
