using System.ComponentModel.DataAnnotations;

namespace PeliculaEntity.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public bool EnCines { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaEstreno { get; set; }

        public HashSet<Comentario>Comentarios { get; set; }=new HashSet<Comentario>(); //Microsoft no garantiza que te ordene los comentarios, para eso usar listado

        public HashSet<Genero>Generos { get; set; }=new HashSet<Genero>(); //Dos Hashset para saltarnos la tabla intermedia o entidad intermedia

        public List<PeliculaActor> PeliculasActores { get; set; }=new List<PeliculaActor>();
    }
}
