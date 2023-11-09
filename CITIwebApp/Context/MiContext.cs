﻿using CITIwebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CITIwebApp.Context
{
    public class MiContext: DbContext
    {
        public MiContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ingeniero> Ingeniero { get; set; }
    }
}
