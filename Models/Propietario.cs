using System.ComponentModel.DataAnnotations;

namespace apiInmobiliariaLabIII.Models
{
    public class Propietario
{
    [Key]
    public int? idPropietario { get; set; }

    [MaxLength(50)]
    public string? nombre { get; set; }

    [MaxLength(50)]
    public string? apellido { get; set; }

    [MaxLength(50)]
    public string? dni { get; set; }

    [MaxLength(50)]
    public string? telefono { get; set; }

    [MaxLength(50)]
    public string? email { get; set; }

    [MaxLength(50)]
    public string? clave { get; set; }
}
}
