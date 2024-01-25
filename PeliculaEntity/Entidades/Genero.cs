using System.ComponentModel.DataAnnotations;

namespace PeliculaEntity.Entidades
{
    public class Genero
    {
        //[Key] //Asi se especifica si es una llave primaria un campo sin poner id (Anotaciones de datos)
        public int Id { get; set; } //Por convencion cuando un campo se llama id se coloca como llave primaria

        //[StringLength(maximumLength:150)]
        public string Nombre { get; set; } = null!; //Se iguala a null para evitar que nos de la advertencia de que no se puede null(Nombre no null)

        //Para ser especifico se usa anotaciones de datos o api fluyente

        public HashSet<Pelicula> Peliculas { get; set; } = new HashSet<Pelicula>();
    }
}
