using Microsoft.EntityFrameworkCore;
using PizzeriaRepository.Data.Interfaces;
using PizzeriaRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaRepository.Data.Implementations
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzeriaDbContext _context;
        public PizzaRepository(PizzeriaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id); // 1. Buscamos la pizza por su ID
            if (pizza == null) return false;

            pizza.Activa = false; // 2. Cambiamos la propiedad 'Activa' a false

            _context.Pizzas.Update(pizza); // 3. Marcamos la entidad para actualización

            return await _context.SaveChangesAsync() > 0; // 4. Impactamos los cambios en SQL Server (ejecuta un UPDATE T_Pizzas SET activa = 0 ...)
        }

        public async Task<List<Pizza>> GetAllAsync()
        {
            return await _context.Pizzas
                                 .Include(p => p.Ingredientes) // Eager Leading con consultas LINQ para traer pizza con sus ingredientes
                                 .Where(p => p.Activa)
                                 .ToListAsync(); // <-- Método asíncrono de EF Core
        }

        public async Task<Pizza?> GetByIdAsync(int id)
        {
            return await _context.Pizzas
                                 .Include(p => p.Ingredientes)
                                 .FirstOrDefaultAsync(p => p.Codigo == id); // <-- Método asíncrono
        }

        public async Task<bool> SaveAsync(Pizza pizza)
        {
            if (pizza.Codigo == 0)
            {
                await _context.Pizzas.AddAsync(pizza); // <-- Opcional, pero recomendable
            }
            else
            {
                _context.Pizzas.Update(pizza); // Update no tiene versión Async porque opera solo en memoria
            }

            return await _context.SaveChangesAsync() > 0; // <-- El guardado en BD es asíncrono
        }
    }
}
