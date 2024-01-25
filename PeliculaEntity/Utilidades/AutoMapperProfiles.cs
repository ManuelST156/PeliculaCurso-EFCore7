using AutoMapper;
using PeliculaEntity.DTOs;
using PeliculaEntity.Entidades;

namespace PeliculaEntity.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<ActorCreacionDTO, Actor>();

            CreateMap<ComentariosCreacionDTO, Comentario>();

            CreateMap<PeliculaCreacionDTO, Pelicula>().ForMember(ent=>ent.Generos, dto=>dto
            .MapFrom(campo=>campo.Generos.Select(
                
                id=>new Genero { Id=id}
                
                )));

            CreateMap<PeliculaActorDTO, PeliculaActor>();

            
        }


    }
}
