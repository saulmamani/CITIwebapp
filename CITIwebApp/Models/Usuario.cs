using CITIwebApp.Dtos;
using System.ComponentModel.DataAnnotations;

namespace CITIwebApp.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? NombreCompleto { get; set; }
        public RolEmun Rol { get; set; }
    }
}
