using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculaEntity.DTOs
{
    public class ActorCreacionDTO
    {

        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        public decimal Fortuna { get; set; }

        public DateTime FechaNacimmiento { get; set; }




    }
}
