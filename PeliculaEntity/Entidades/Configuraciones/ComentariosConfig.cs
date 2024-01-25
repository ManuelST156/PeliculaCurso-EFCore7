using Microsoft.EntityFrameworkCore;

namespace PeliculaEntity.Entidades.Configuraciones
{
    public class ComentariosConfig : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comentario> builder)
        {
            builder.Property(c => c.Contenido).HasMaxLength(500);
        }
    }
}
