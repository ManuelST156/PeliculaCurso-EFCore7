using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculaEntity.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        //Utilizamos esto para insertar data sin tener que ir al swagger e insertarla manualmente
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            var historia = new Genero { Id = 7 , Nombre="Historia"};
            var animacion= new Genero { Id = 8, Nombre = "Animacion" };
            builder.HasData(historia, animacion);

            builder.HasIndex(p=>p.Nombre).IsUnique();
        }
    }
}
