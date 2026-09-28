using PizzeriaRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaRepository.Data.Interfaces
{
    public interface IPizzaRepository
    {
        Task<List<Pizza>> GetAllAsync();
        Task<Pizza?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Pizza pizza);
        Task<bool> DeleteAsync(int id);
    }
}
