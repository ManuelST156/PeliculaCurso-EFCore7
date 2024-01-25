using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PeliculaEntity.DTOs
{
    public class PeliculaCreacionDTO
    {
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime FechaEstreno { get; set; }
        public List<int>Generos { get; set; }=new List<int>();
        public List<PeliculaActorDTO> PeliculasActores { get; set; } = new List<PeliculaActorDTO>(); 




    }
}
