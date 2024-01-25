using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace PeliculaEntity.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            //modelBuilder.Entity<Pelicula>().Property(p => p.Titulo).HasMaxLength(150);
            builder.Property(p => p.FechaEstreno).HasColumnType("date");


            
        }
    }
}
