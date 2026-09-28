using Microsoft.EntityFrameworkCore;
using PizzeriaRepository.Domain; // asegurarse de que se agregue el using del dominio
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaRepository.Data
{
    public class PizzeriaDbContext : DbContext
    {
        // DbContext: Clase principal que representa la sesión con la base de datos y contiene los DbSet<TEntity>

        // El constructor recibe las opciones de configuración (como la Connection String)
        // Configuracion que se le pasa al contexto desde el program.cs de la WebApi para que use sqlServer y la cadena de conexion appsettings.json
        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options) : base(options) 
        {
        }

        // Mapeo de las tablas a conjuntos de entidades (DbSet)
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<IngredientePizza> Ingredientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // aca reforzamos incluso las data annottations 
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo explícito de la relación 1 a N mediante Fluent API (interfaz fluida)
            modelBuilder.Entity<IngredientePizza>()
                .HasOne(i => i.Pizza)
                .WithMany(p => p.Ingredientes) // Relaciones entre Entidades
                .HasForeignKey(i => i.CodigoPizza);
        }
    }
}
