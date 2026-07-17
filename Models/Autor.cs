



using System.ComponentModel.DataAnnotations;

public class Autor
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(maximumLength:100, MinimumLength = 3, ErrorMessage = "Minimo 3 y Maximo 100 caracteres")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "La nacionalidad es requerida")]
    [StringLength(maximumLength:50, ErrorMessage = "Maximo 50 caracteres")]
    public string Nacionalidad { get; set; }
    [Range(minimum:1500, maximum:2100, ErrorMessage = "El Anio debe estar entre 1500 y 2100")]
    public int AnioNacimiento { get; set; }
}