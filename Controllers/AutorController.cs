



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica3_libros.Context;

namespace Practica3_libros.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutorController : ControllerBase
    {
        private readonly LibrosDbContext _context;

        public AutorController(LibrosDbContext context)
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

        // #1. Obtener todos los autores - GET
        [HttpGet()]
        public async Task<IActionResult> ObtenerAutores()
        {
            var autores = await _context.Autor.ToListAsync();
            return StatusCode(200, ReponseData("Todos los autores", autores));
        }

        // #2. Devolver un libro especifico por su Id - GET
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> ObtenerAutorPorId(int Id)
        {
            var autor = await _context.Autor.FirstOrDefaultAsync(l => l.Id == Id);
            if (autor == null) return StatusCode(404, ReponseData($"El autor con Id: {Id} no se encontro", null));

            return StatusCode(200, ReponseData("Libro encontrado", autor));
        }

        // #3. Registrar un libro - POST
        [HttpPost()]
        public async Task<IActionResult> RegistrarAutor([FromBody] Autor autor)
        {
            var autorFind = await _context.Autor.FirstOrDefaultAsync(l => l.Nombre == autor.Nombre);
            if (autorFind != null) return StatusCode(400, ReponseData("El autor ya existe", null));

            _context.Autor.Add(autor);
            var ok = await _context.SaveChangesAsync();
            if (ok < 0) return StatusCode(500, ReponseData("Algo salio mal al registrar el autor", autor));

            return StatusCode(201, ReponseData("autor registrado con exito", autor));
        }

        // #4. Actualizar libro - PUT
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> ActualizarAutor(int Id,  [FromBody] Autor autor)
        {
            var autorFind = await _context.Autor.FirstOrDefaultAsync(l => l.Id == Id);
            if (autorFind == null) return StatusCode(404, ReponseData("autor no encontrado", null));

            autorFind.Nombre = autor.Nombre;
            autorFind.AnioNacimiento = autor.AnioNacimiento;
            autorFind.Nacionalidad = autor.Nacionalidad;

            _context.Autor.Update(autorFind);
            await _context.SaveChangesAsync();

            return StatusCode(204);
        }

        // #5. Elimininar un libro - DELETE
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> EliminarLibroAutor(int Id)
        {
            var autor = await _context.Autor.FirstOrDefaultAsync(l => l.Id == Id);
            if (autor == null) return StatusCode(404, ReponseData("autor no encontrado", null));

            _context.Autor.Remove(autor);
            var ok = await _context.SaveChangesAsync();
            if (ok <= 0) return StatusCode(500, ReponseData("Algo salio mal al eliinar el autor", autor));

            return StatusCode(204);
        }


         // #6. Devolver los libros pertenecientes a un autor - GET
        [HttpGet("{Id:int}/libros")]
        public async Task<IActionResult> ObtenerLibrosPorAutor(int Id)
        {
            var autor = await _context.Autor.FirstOrDefaultAsync(l => l.Id == Id);
            if (autor == null) return StatusCode(404, ReponseData("Autor no encontrado", null));

            var libros = await _context.Libro.Where(l => l.AutorId == Id).ToListAsync();
            return StatusCode(200, ReponseData($"Libro por autor - {autor.Nombre}", libros));
        }

        // #7. Devolver los autores paginados - GET
        [HttpGet("paginados")]
        public async Task<IActionResult> ObtenerAutoresPaginados([FromQuery] int? pagina, [FromQuery] int? tamanoPagina, [FromQuery] string? buscar, [FromQuery] string? ordenarPor, [FromQuery] string? direccion)
        {
            if(pagina == null || pagina == 0) return StatusCode(400, ReponseData($"La pagina debe ser mayor que cero {pagina}", null));
            if(tamanoPagina == null || tamanoPagina == 0) return StatusCode(400, ReponseData($"El tamano debe ser mayor que cero {pagina}", null));
            if(string.IsNullOrEmpty(buscar)) return StatusCode(400, ReponseData($"El termino de busqueda es requerido {pagina}", null));
            if(string.IsNullOrEmpty(ordenarPor)) return StatusCode(400, ReponseData($"El campo de orden es requerido {pagina}", null));
            if(string.IsNullOrEmpty(direccion)) return StatusCode(400, ReponseData($"La direccon del orden es requerido {pagina}", null));

            var query = _context.Autor
            .Where(a => a.Nombre.ToLower().Contains(buscar.ToLower()))
            .Skip(((pagina ?? 1) - 1) * (tamanoPagina ?? 10))
            .Take(tamanoPagina ?? 10);
            
            if(!string.IsNullOrEmpty(direccion))
            {
                if(direccion.Equals("desc", StringComparison.CurrentCultureIgnoreCase)) query = query.OrderByDescending(a => EF.Property<object>(a, ordenarPor));

                if(direccion.Equals("asc", StringComparison.CurrentCultureIgnoreCase)) query = query.OrderBy(a => EF.Property<object>(a, ordenarPor));
            }
            
            var autores = await  query.ToListAsync();
            return StatusCode(200, ReponseData($"Libro por autor", autores));
        }
    }
}