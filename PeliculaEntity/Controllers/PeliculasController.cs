using AutoMapper;
using PeliculaEntity.DTOs;
using PeliculaEntity.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PeliculaEntity.Controllers
{

    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper) //usar siempre ctor
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost] //Endpoint
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            

            if(pelicula.Generos is not null)
            {
                foreach (var genero in pelicula.Generos)
                {
                    context.Entry(genero).State = EntityState.Unchanged;
                    /*
                     Esto permite que el applicationdbcontext pueda dar seguimiento a los distintos objetos que tengo en la aplicacion para determinar
                    cuales de ellos se corresponde con registro de la base de datos. Aqui tiene un estatus sin cambiar osea un objeto que no ha realizado ningun cambio
                    en la db, porque son datos existentes.
                    
                    added: estatus cuando esta agregado el savechanges lo que hace es agregar el dato y luego usa un entry internamente para asi agregarlo

                     Esto cuando hacemos la relacion de mucho a mucho cuando nos saltamos la entidad intermedia
                     */




                }
            }

            if(pelicula.PeliculasActores is not null)
            {

                /*En la configuracion de peliculas y actores utilizamos una entidad intermedia por ende el proceso anterior con genero no es necesario
                 Se crea un for para marcar un orden a los actores de la pelicula segun el orden en que los recibo
                 */
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();



        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> Get(int id)
        {
            var pelicula= await context.Peliculas.Include(p=>p.Comentarios)
                .Include(p=>p.Generos)
                .Include(p=>p.PeliculasActores.OrderBy(pa=>pa.Orden))
                    .ThenInclude(p=>p.Actor)
                .FirstOrDefaultAsync(p=>p.Id==id); 
            //Metodo Eager Loading para traer data relacionada de una tabla

            if(pelicula is null)
            {
                return NotFound();
            }

            return pelicula;
        }

        [HttpGet("select/{id:int}")]
        public async Task<ActionResult> GetSelect(int id)
        {

            //Metodo Select Loading para traer data relacionada de una tabla, diferenciandose porque nos permite traer data especifica

            var pelicula = await context.Peliculas
                .Select(pel => new
                {
                    pel.Id,
                    pel.Titulo,
                    Generos = pel.Generos.Select(g => g.Nombre).ToList(),
                    Actores = pel.PeliculasActores.OrderBy(pa => pa.Orden).Select(pa =>
                    new
                    {
                        Id = pa.ActorId,
                        pa.Actor.Nombre,
                        pa.Personaje
                    }),
                    CantidadComentarios = pel.Comentarios.Count()

                })
                .FirstOrDefaultAsync(p=>p.Id==id);


           

            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula); //Esto es porque al proyectarse a un tipo anonimo se utiliza return ok
        }

        [HttpGet("select/nombre")]
        public async Task<ActionResult> GetSelectNombre(string nombre)
        {

            //Metodo Select Loading para traer data relacionada de una tabla, diferenciandose porque nos permite traer data especifica

            var pelicula = await context.Peliculas
                .Select(pel => new
                {
                    pel.Id,
                    pel.Titulo,
                    Generos = pel.Generos.Select(g => g.Nombre).ToList(),
                    Actores = pel.PeliculasActores.OrderBy(pa => pa.Orden).Select(pa =>
                    new
                    {
                        Id = pa.ActorId,
                        pa.Actor.Nombre,
                        pa.Personaje
                    }),
                    CantidadComentarios = pel.Comentarios.Count()

                })
                .FirstOrDefaultAsync(p => p.Titulo == nombre);




            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula); //Esto es porque al proyectarse a un tipo anonimo se utiliza return ok
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {

            //Metodo Select Loading para traer data relacionada de una tabla, diferenciandose porque nos permite traer data especifica

            var pelicula = await context.Peliculas
                .Select(pel => new
                {
                    pel.Id,
                    pel.Titulo,
                    pel.EnCines,
                    pel.FechaEstreno,
                    Generos = pel.Generos.Select(g => g.Nombre).ToList(),
                    peliculasActores = pel.PeliculasActores.OrderBy(pa => pa.Orden).Select(pa =>
                    new
                    {
                        Id = pa.ActorId,
                        pa.Actor.Nombre,
                        pa.Personaje
                    }),
                    CantidadComentarios = pel.Comentarios.Count()

                })
                .ToListAsync();




            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula); //Esto es porque al proyectarse a un tipo anonimo se utiliza return ok
        }






        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {

            //Forma moderna con entity framework core 7
            //Diferencia: Aqui solo se hace un query porque desde que se verifica de que existe se borra
            var filasAlteradas = await context.Peliculas.Where(g => g.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            return NoContent(); //Metodo como return Ok()


        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaExistente = await context.Peliculas
                .Include(p => p.Generos)
                .Include(p => p.PeliculasActores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (peliculaExistente is null)
            {
                return NotFound();
            }

            // Actualiza las propiedades de la película con los valores del DTO
            mapper.Map(peliculaCreacionDTO, peliculaExistente);

            // Actualiza los géneros
            if (peliculaCreacionDTO.Generos != null)
            {
                var generos = await context.Generos
                    .Where(g => peliculaCreacionDTO.Generos.Contains(g.Id))
                    .ToListAsync();

                peliculaExistente.Generos = new HashSet<Genero>(generos);
            }
            else
            {
                peliculaExistente.Generos.Clear(); // Elimina los géneros existentes
            }

            // Actualiza los actores
            if (peliculaCreacionDTO.PeliculasActores != null)
            {
                for (int i = 0; i < peliculaCreacionDTO.PeliculasActores.Count; i++)
                {
                    var actorDTO = peliculaCreacionDTO.PeliculasActores[i];
                    if (i < peliculaExistente.PeliculasActores.Count)
                    {
                        // Si existe una entidad intermedia con el mismo índice, actualiza sus propiedades
                        var peliculaActor = peliculaExistente.PeliculasActores[i];
                        mapper.Map(actorDTO, peliculaActor);
                    }
                    else
                    {
                        // Si no existe una entidad intermedia con el mismo índice, crea una nueva
                        var peliculaActor = mapper.Map<PeliculaActor>(actorDTO);
                        peliculaActor.Orden = i + 1;
                        peliculaExistente.PeliculasActores.Add(peliculaActor);
                    }
                }

                // Elimina las entidades intermedias sobrantes (si el DTO tiene menos elementos)
                for (int i = peliculaCreacionDTO.PeliculasActores.Count; i < peliculaExistente.PeliculasActores.Count; i++)
                {
                    context.Remove(peliculaExistente.PeliculasActores[i]);
                }
            }
            else
            {
                // Elimina todas las entidades intermedias si el DTO no contiene ninguna
                context.RemoveRange(peliculaExistente.PeliculasActores);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }



    }
}
