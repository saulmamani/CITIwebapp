using CITIwebApp.Dtos;

namespace CITIwebApp.Models
{
    public class Ingeniero
    {
        public int Id { get; set; }
        public int Rni { get; set; }
        public string? Ci { get; set; }
        public string? NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public EspecialidadEnum Especialidad { get; set; }
    }
}
