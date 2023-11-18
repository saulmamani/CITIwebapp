using CITIwebApp.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CITIwebApp.Models
{
    public class Ingeniero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Rni { get; set; }
        [Required]
        public string? Ci { get; set; }
        [Required]
        public string? NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Monto { get; set; }
        public string? Foto { get; set; } //almacenar la foto

        [Required]
        public EspecialidadEnum Especialidad { get; set; }

        //Solo de ayuda para cargar la foto
        [NotMapped]
        [Display(Name = "Cargar Foto")]
        public IFormFile? FotoFile { get; set; } //cargar la foto de la UI
    }
}
