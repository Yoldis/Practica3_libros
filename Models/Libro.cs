using System.ComponentModel.DataAnnotations;

namespace Practica3_libros.Models
{
    public class Libro

    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El titulo es requerido")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El autor es requerido")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "El anio es requerido")]
        public int AnioPublicacion { get; set; }
        [Required(ErrorMessage = "El genero es requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El numero de pagina es requerido")]
        public int NumeroPaginas { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

    }
}
