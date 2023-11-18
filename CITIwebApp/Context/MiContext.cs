using CITIwebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CITIwebApp.Context
{
    public class MiContext: DbContext
    {
        public MiContext(DbContextOptions options): base(options)
        {
            
        }

        //Estas clases persistentes, se van a transformar en tablas en la BDD
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ingeniero> Ingeniero { get; set; }
    }
}
