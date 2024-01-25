using AutoMapper;
using PeliculaEntity.DTOs;
using PeliculaEntity.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PeliculaEntity.Controllers
{


    [ApiController]
    [Route("api/peliculas/comentarios/")]
    public class ComentariosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("{peliculaId:int}")]
        public async Task<ActionResult> Post(int peliculaId,ComentariosCreacionDTO comentariosCreacionDTO)
        {
            var comentario = mapper.Map<Comentario>(comentariosCreacionDTO);
            comentario.PeliculaId= peliculaId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> Get()
        {

            return await context.Comentarios.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> Get(int id)
        {
           
            return await context.Comentarios.Where(a => a.PeliculaId == id).ToListAsync();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

       
            var filasAlteradas = await context.Comentarios.Where(g => g.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            return NoContent(); //Metodo como return Ok()


        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ComentariosCreacionDTO comentariosCreacionDTO)
        {
            var comentario = await context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

            if (comentario == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del comentario con los valores de comentariosCreacionDTO
            mapper.Map(comentariosCreacionDTO, comentario);

            context.Update(comentario);
            await context.SaveChangesAsync();

            return Ok();
        }





    }
}
