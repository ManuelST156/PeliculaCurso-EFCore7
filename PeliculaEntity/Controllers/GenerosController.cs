using AutoMapper;
using PeliculaEntity.DTOs;
using PeliculaEntity.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PeliculaEntity.Controllers
{

    [ApiController]
    [Route("api/generos")]
    public class GenerosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        //Los controladores son los que se encargan de recibir peticiones http, ejecutando el metodo de esa clase.
        //Esto tiene que ver con asp.net core. Usar la inyeccion de dependencias para obtener una instancia del applicationdbcontext

        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("idynombre")]
        public async Task<ActionResult<Genero>>Getidynombre()
        {
            var generos = await context.Generos.Select(g => new { g.Id, g.Nombre }).ToListAsync();
            return Ok(generos);

            //Se utiliza para traer en forma de lista inumerable de losd atos de generos de la tabla generos
        }





        [HttpPost]//Este metodo se ejecutara cuando se haga una peticion desde la url api/generos
        public async Task<ActionResult> Post(GeneroCreacionDTO generoCreacion)//Es buena practica utilizar programacion asincronas con programcion IO, que es cuando nuestro sistema se comunica con otro sistema
        {//Endpoint

           
            var yaExisteGeneroConEsteNombre=await context.Generos.AnyAsync(g=>g.Nombre==generoCreacion.Nombre);

            if(yaExisteGeneroConEsteNombre)
            {
                return BadRequest("Ya existe un genero con este nombre" + generoCreacion.Nombre);
            }

            var genero= mapper.Map<Genero>(generoCreacion);
            context.Add(genero);
            await context.SaveChangesAsync(); //Se coloca en esta linea porque la funcion que se comunicara con la DB para insertar el genero es savechanges porque va a empujar los cambios recogidos a la DB
            return Ok();
        }

        [HttpPost("Varios")]
        public async Task<ActionResult> Post(GeneroCreacionDTO[] generosCreacionDTO)
        {
            //Action result en asp core son las distintas cosas que podemos retornar como una pag html o json


            var generos= mapper.Map<Genero[]>(generosCreacionDTO);
            context.AddRange(generos);//Se utiliza este arreglo para guardar varios datos
            await context.SaveChangesAsync();//Todas las operaciones de agregado se trabajan de forma atomica, o todas funcionan o todas fallan
            //Savechanges maneja transacciones
            return Ok();
        }

        [HttpPut("{id:int}/nombre2")]//se utiliza put para actualizar registros

        public async Task<ActionResult>Put(int id)
        {

            /*Este es el metodo conectado se llama asi porque a traves de un db context estoy obteniendo un registro y luego lo actualizo a traves del mismo
             * db context. Es conectado porque es la misma instancia*/
            var genero= await context.Generos.FirstOrDefaultAsync(g=>g.Id==id);

            if(genero is null)
            {
                return NotFound();
            }

            genero.Nombre = genero.Nombre + "2"; //Permite cambiar los nombres directamente ddesde el codigo

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(int id, GeneroCreacionDTO generoCreacionDTO)
        {

            /*Metodo desconectado, aqui lo que se hace es instanciando un nuevo genero, es decir, 
             * se cargo con un db context que tiene que actualizar un genero que no conoce
             * 
             * Con el add, lo que hacia era marcar el objeto como agregado para que el savechange lo agregue
             * 
             * Con el update lo que hago es marcar el objeto como actualizado para con el savechange actualizar un objeto existente
            */
            var genero= mapper.Map<Genero>(generoCreacionDTO);
            genero.Id= id;
            context.Update(genero);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult>Delete(int id)
        {

            //Forma moderna con entity framework core 7
            //Diferencia: Aqui solo se hace un query porque desde que se verifica de que existe se borra
            var filasAlteradas= await context.Generos.Where(g=>g.Id==id).ExecuteDeleteAsync();

            if(filasAlteradas==0)
            {
                return NotFound();
            }
            return NoContent(); //Metodo como return Ok()


        }

        [HttpDelete("{id:int}/anterior")]
        public async Task<ActionResult>DeleteAnterior(int id)
        {
            //Forma antigua con entity framework versiones inferiores a 7
            //Diferencia: Aqui se asegura de que exista, hace dos querys, trae la data y luego la borra
            //Este se puede utilizar por ejemplo si necesito hacer algo con la data antes de borrarlo
            var genero= await context.Generos.FirstOrDefaultAsync(g=>g.Id==id);

            if(genero is null)
            {
                return NotFound();
            }

            context.Remove(genero);
            await context.SaveChangesAsync();

            return NoContent(); 
        }

        






    }
}
