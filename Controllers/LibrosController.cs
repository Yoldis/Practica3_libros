using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica3_libros.Context;
using Practica3_libros.Models;

namespace Practica3_libros.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly LibrosDbContext _context;

        public LibrosController(LibrosDbContext context)
        {
            _context = context;
        }

        private dynamic ReponseData(string mensaje, dynamic? data)
        {
            return new
            {
                Mensaje = mensaje,
                Data = data
            };
        }

        // #1. Obtener todos los libros - GET
        [HttpGet()]
        public async Task<IActionResult> ObtenerLibros()
        {
            var libros = await _context.Libro.ToListAsync();
            return StatusCode(200, ReponseData("Todos los libros", libros));
        }

        // #2. Devolver un libro especifico por su Id - GET
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> ObtenerlibroPorId(int Id)
        {
            var libro = await _context.Libro.FirstOrDefaultAsync(l => l.Id == Id);
            if (libro == null) return StatusCode(404, ReponseData($"El libro con Id: {Id} no se encontro", null));

            return StatusCode(200, ReponseData("Libro encontrado", libro));
        }

        // #3. Registrar un libro - POST
        [HttpPost()]
        public async Task<IActionResult> RegistrarLibro([FromBody] Libro libro)
        {
            var libroFind = await _context.Libro.FirstOrDefaultAsync(l => l.Titulo == libro.Titulo);
            if (libroFind != null) return StatusCode(400, ReponseData("El libro ya existe", null));

            _context.Libro.Add(libro);
            var ok = await _context.SaveChangesAsync();
            if (ok < 0) return StatusCode(500, ReponseData("Algo salio mal al registrar el libro", libro));

            return StatusCode(201, ReponseData("Libro registrado con exito", libro));
        }

        // #4. Actualizar libro - PUT
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> ActualizarLibro(int Id,  [FromBody] Libro libro)
        {
            var libroFind = await _context.Libro.FirstOrDefaultAsync(l => l.Id == Id);
            if (libroFind == null) return StatusCode(404, ReponseData("Libro no encontrado", null));

            libroFind.Titulo = libro.Titulo;
            libroFind.Autor = libro.Autor;
            libroFind.AnioPublicacion = libro.AnioPublicacion;
            libroFind.Genero = libro.Genero;
            libroFind.NumeroPaginas = libro.NumeroPaginas;
            libroFind.Precio = libro.Precio;
            libroFind.Disponible = libro.Disponible;

            _context.Libro.Update(libroFind);
            await _context.SaveChangesAsync();

            return StatusCode(204);
        }

        // #5. Elimininar un libro - DELETE
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> EliminarLibro(int Id)
        {
            var libro = await _context.Libro.FirstOrDefaultAsync(l => l.Id == Id);
            if (libro == null) return StatusCode(404, ReponseData("Libro no encontrado", null));

            _context.Libro.Remove(libro);
            var ok = await _context.SaveChangesAsync();
            if (ok <= 0) return StatusCode(500, ReponseData("Algo salio mal al eliinar el libro", libro));

            return StatusCode(204);
        }
    }
}
