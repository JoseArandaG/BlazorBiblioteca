using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BlazorBiblioteca.Context;
using Microsoft.EntityFrameworkCore;
using BlazorBiblioteca.Shared;

namespace BlazorBiblioteca.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        //Inicializacion Context
        private readonly LibroDBContext _context;

        public LibroController(LibroDBContext context)
        {
            _context = context;
        }

        //Peticon HttpGet que conecta con el servidor
        [HttpGet("ConexionUsuarios")]
        public async Task<ActionResult<string>> GetConexionUsuarios()
        {
            try
            {
                var respuesta = await _context.Libros.ToListAsync();
                return "Conectado a la base de datos     ";
            }
            catch
            {
                return "Error de conexion con Base de datos";
            }
        }

        [HttpGet("ConexionLibros")]
        public async Task<ActionResult<string>> ConexionLibros()
        {
            try
            {
                var respuesta = await _context.Libros.ToListAsync();
                return "Conectado a la base de datos y a la tabla Libros";
            }
            catch
            {
                return "Error de conexión con la tabla Libros";
            }
        }


        //Agregar Libros
        [HttpPost("InsertarLibro")]
        public async Task<ActionResult<string>> HandleCreateLibro([FromBody] Libro libro)
        {
            await _context.Libros.AddAsync(libro);
            var res = await _context.SaveChangesAsync();

            if (res == 1) return Created();
            else return BadRequest();
        }

        //Metodo de Listado de Libros.
        [HttpGet("LibrosListado")]
        public async Task<ActionResult<List<Libro>>> ListarLibros()
        {
            var res = await _context.Libros.ToListAsync();
            return res;
        }

        //Metodo Eliminar Libros
        [HttpDelete("libro/{id}")]

        public async Task<ActionResult<string>> HandleDeleteLibro([FromRoute] int id)
        {
            var find = await _context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (find == 1) return Ok();
            else return BadRequest();
        }

        //Metodo Actualizar Libros
        [HttpPut("libro/{id}")]
        public async Task<IActionResult> Put(int id, Libro libroActualizado)
        {
            if (id != libroActualizado.Id)
            {
                return BadRequest("El ID del libro no coincide con la URL.");
            }

            var libroExistente = await _context.Libros.FindAsync(id);
            if (libroExistente == null)
            { 
                return NotFound();
            }
            libroExistente.NombreLibro = libroActualizado.NombreLibro;
            libroExistente.Autor = libroActualizado.Autor;
            libroExistente.NumPaginas = libroActualizado.NumPaginas;
            libroExistente.FechaPublicacion = libroActualizado.FechaPublicacion;

            await _context.SaveChangesAsync();

            return Ok(libroExistente);

        }

    }
}
