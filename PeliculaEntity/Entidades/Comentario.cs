namespace PeliculaEntity.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string? Contenido { get; set; }
        public bool Recomendar { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set;}=null!; //Propiedad de navegacion que va desde pelicula hacia comentario
    }
}
