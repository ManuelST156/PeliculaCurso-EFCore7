using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace PeliculaEntity.Entidades.Configuraciones
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            //modelBuilder.Entity<Actor>().HasKey(g => g.Id);
            builder.Property(a => a.Fortuna).HasPrecision(18, 2);
            //modelBuilder.Entity<Actor>().Property(a => a.Nombre).HasMaxLength(150);
            builder.Property(a => a.FechaNacimmiento).HasColumnType("date");
            //builder.Property(a=>a.FechaNacimmiento).
        }
    }
}
