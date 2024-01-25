using PeliculaEntity.Entidades;
using PeliculaEntity.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PeliculaEntity
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           
        }

        //Esta forma de configuracion es para utilizarr el api fluyente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //no se borra

            modelBuilder.Entity<Genero>().HasKey(g => g.Id); //Con esto se especifica la llave primaria de id
            //modelBuilder.Entity<Genero>().Property(g => g.Nombre).HasMaxLength(150);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Se utiliza para poder llamar las configuraciones de mi
            //interfaz IEntityTypeConfiguration que estan ubicadas en clases o archivos de configuracion

            SeedingInicial.Seed(modelBuilder);
            //Nos permite traer desde la clase seedingInicial que contiene variables con los datos a insertar en la tabla

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)//esto es para configurar las notaciones de datos
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().HaveMaxLength(150);
            
            
        }


        public DbSet<Genero> Generos => Set<Genero>(); //Estoy guardando la clase genero como entidad
        public DbSet<Actor> Actores => Set<Actor>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Comentario> Comentarios => Set<Comentario>();
        public DbSet<PeliculaActor> PeliculasActores=> Set<PeliculaActor>();

        /*
        Para hacer la relacion en entity se puede utilizar la configuracion por convencion.Una propiedad de navegacion nos permite obtener
        la data relacionada de una entidad de forma sencilla.
        */


    }
}


//En entity hay tres formas de configuracion: por convencion, por anotaciones de datos, y por api fluyente.
