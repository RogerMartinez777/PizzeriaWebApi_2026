using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaRepository.Domain
{
    // Usamos Data Annotations - proporciona metadatos a las propiedades de clases del Dominio
    // Indicandole a EF como mapear los objetos contra la BD
    [Table("T_Pizzas")]
    public class Pizza
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Required]
        [Column("n_pizza")]
        public string Nombre { get; set; } = string.Empty;

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("esta_activa")]
        public bool Activa { get; set; } = true;

        // Relación 1 a N: Una pizza tiene una lista de ingredientes
        public virtual List<IngredientePizza> Ingredientes { get; set; } = new List<IngredientePizza>();
    }
}
