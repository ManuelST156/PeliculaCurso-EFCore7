using System.ComponentModel.DataAnnotations;

namespace PeliculaEntity.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public decimal Fortuna { get; set;}



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = " {0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimmiento{ get; set; }

        public List<PeliculaActor> PeliculasActores { get; set; } = new List<PeliculaActor>();
    }
}
