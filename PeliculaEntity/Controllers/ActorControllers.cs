using AutoMapper;
using AutoMapper.QueryableExtensions;
using PeliculaEntity.DTOs;
using PeliculaEntity.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace PeliculaEntity.Controllers
{

    [ApiController]
    [Route("api/actores")]
    public class ActorControllers: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorControllers(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {

            //Trae todos los actores de la tabla actores
            return await context.Actores.ToListAsync();
        }

        [HttpGet ("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>>Get(string nombre)
        {
            /*Version 1: Se utiliza para traer la lista de actores con el filtro de nombres, este proceso funciona asi: busca que su nombre sea
             igual al nombre que se esta recibiendo y los trae como lista. --query
             */
            return await context.Actores.Where(a=>a.Nombre==nombre).ToListAsync();
        }


        [HttpGet("nombre/v2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV2(string nombre)
        {
            /*Version 2: Se utiliza para traer la lista de actores con el filtro de nombres, este proceso funciona asi: busca que su nombre contengan
             elementos del nombre que se esta recibiendo y los trae como lista. --query
             */
            return await context.Actores.Where(a => a.Nombre.Contains(nombre)).ToListAsync();
        }


        [HttpGet("fechaNacimiento/rango")]
        public async Task<ActionResult<IEnumerable<Actor>>>Get(DateTime inicio, DateTime fin)
        {
            /*Busca actores segun el rango de fechas
             */


            return await context.Actores.Where(a => a.FechaNacimmiento >= inicio && a.FechaNacimmiento <=fin).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> Get(int id)
        {
            /*Mediante el metodo firstordefault busca el actor por el id, el primero o por defecto*/
            var actor= await context.Actores.FirstOrDefaultAsync(a=>a.Id==id);

            if (actor is null)
            {
                return NotFound();
            }

            return actor;
        }


        [HttpGet("ordenados")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV3()
        {
            //return await context.Actores.OrderBy(a=>a.FechaNacimmiento).ToListAsync();
            return await context.Actores.OrderByDescending(a => a.FechaNacimmiento).ToListAsync();
        }

        [HttpGet("nombre/fechaNacimiento/ordenados")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV4(string nombre)
        {
            /*Ordenar por nombre y por fechanacimiento
             */
            return await context.Actores.Where(a => a.Nombre == nombre).OrderBy(a=>a.FechaNacimmiento)
                .ThenBy(a=>a.Nombre).ToListAsync();
        }

        [HttpGet("idynombre")]
        public async Task<ActionResult>Getidynombre()
        {
            /*Lo que hace este get es buscar los campos de nombre y id, nos dirigimos a la tabla actores,
             donde usamos una proyeccion hacia un objeto anonimo, donde hace un query de un select donde se devuelve la busqueda
            y la respuesta la serializa a json porque se esta proyectando a un objeto anonimo y cuando se use dto se llama un ok
            */

            var actores=await context.Actores.Select(a=>new {a.Id, a.Nombre }).ToListAsync();
            return Ok(actores);
        }

        [HttpGet("idynombreClase")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> Getidynombreclase()
        {
            /*Lo que hace es buscar los campos nombre y id, pero a traves de una clase, en este caso clase actoresDTO
            */

            return await context.Actores.Select(a => new ActorDTO { Id=a.Id, Nombre=a.Nombre }).ToListAsync();
            
        }

        [HttpGet("idynombreAutoMapper")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> Getidynombreautomapper()
        {
            /*Hace lo mismo pero con la integracion de automapper, es decir automapper sabe que campos utilizamos por los datos en dto
            */

            return await context.Actores.ProjectTo<ActorDTO>(mapper.ConfigurationProvider).ToListAsync();

        }




        [HttpPost]
        public async Task<ActionResult>Post(ActorCreacionDTO actorCreacionDTO)
        {
            var actor=mapper.Map<Actor>(actorCreacionDTO);  
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ActorCreacionDTO actorCreacionDTO)
        {

            var actores = mapper.Map<Actor>(actorCreacionDTO);
            actores.Id = id;
            context.Update(actores);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            
            var filasAlteradas = await context.Actores.Where(g => g.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            return NoContent(); 


        }



    }
}
