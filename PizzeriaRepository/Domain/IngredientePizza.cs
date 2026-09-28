using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PizzeriaRepository.Domain
{
    // Usamos Data Annotations - proporciona metadatos a las propiedades de clases del Dominio
    // Indicandole a EF como mapear los objetos contra la BD
    [Table("T_Ingredientes")] // Mapea la clase C# con el nombre exacto de la tabla en SQL
    public class IngredientePizza
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Required]
        [Column("n_ingrediente")]
        public string NombreIngrediente { get; set; } = string.Empty;

        [Column("cantidad")]
        public double? Cantidad { get; set; }

        [Column("unidad")]
        public string? Unidad { get; set; }

        // Clave Foránea
        [Column("codigo_pizza")]
        public int CodigoPizza { get; set; }

        // Propiedad de navegación inversa (evita ciclos de serialización JSON)
        [JsonIgnore]
        [ForeignKey("CodigoPizza")]
        public virtual Pizza? Pizza { get; set; } // el signo ? mantiene nullable el codigopizza
    }
}
