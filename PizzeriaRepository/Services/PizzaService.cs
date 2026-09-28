using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// no olvidar agregar los using
using PizzeriaRepository.Data.Interfaces;
using PizzeriaRepository.Domain;

namespace PizzeriaRepository.Services
{
    public class PizzaService
    {
        private readonly IPizzaRepository _repository;

        // Inyecta directamente la interfaz del repositorio
        public PizzaService(IPizzaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pizza>> ObtenertodasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pizza?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) return null;
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> GuardarAsync(Pizza pizza)
        {
            // Validaciones de negocio (siguen siendo síncronas en memoria)
            if (string.IsNullOrWhiteSpace(pizza.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (pizza.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a $0.");

            return await _repository.SaveAsync(pizza);
        }

        public async Task<bool> DarDeBajaAsync(int id)
        {
            if (id <= 0) return false;

            // Espera la respuesta asíncrona del repositorio
            return await _repository.DeleteAsync(id);
        }
    }
}
