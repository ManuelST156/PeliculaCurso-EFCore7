using System.ComponentModel.DataAnnotations;

namespace PeliculaEntity.DTOs
{
    public class GeneroCreacionDTO
    {
        /*
         Swager me representa el grafo de objetos que tiene una entidad. Esta es una señal de que el metodo post estoy recibiendo demasiada info.
         Se puede cambiar la clase genero por un dto que es un data transfer Object(Objeto de transferencia de datos), son clases que usan para 
         sostener la data que vamos a pasar de un lugar a otro y nos permite controlar la estructura que vamos a recibir y enviar de nuestros metodos
         y ademas puedo esconder la entidad del mundo exterior para controlar que quiero que se muestre o no.
         
        */

        [StringLength(maximumLength: 150)]
        public string Nombre { get; set; } = null!;


    }
}
