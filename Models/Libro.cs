using System.ComponentModel.DataAnnotations;

namespace Practica3_libros.Models
{
    public class Libro

    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El titulo es requerido")]
        [StringLength(maximumLength:100, MinimumLength = 1, ErrorMessage = "Minimo 1 y Maximo 100 caracteres")]
        public string Titulo { get; set; }
        [Range(minimum:1450, maximum:2100, ErrorMessage = "Entre 1450 y 2100")]
        public int AnioPublicacion { get; set; }
        [Required(ErrorMessage = "El genero es requerido")]
        [StringLength(maximumLength:50, ErrorMessage = "Maximo 50 caracteres")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El numero de pagina es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor que cero")]
        public int NumeroPaginas { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0, 999999999, ErrorMessage = "El precio debe ser mayor o igual a cero.")]
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        [Required(ErrorMessage = "El autor es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor que cero")]
        public int AutorId { get; set; }
         // Falta esta propiedad de navegación
    }
}
