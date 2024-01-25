using Microsoft.EntityFrameworkCore;

namespace PeliculaEntity.Entidades.Seeding
{
    public class SeedingInicial
    {
        public static void Seed(ModelBuilder modelBuilder)
        {

            //Actores

            var samuelJackson = new Actor()
            {
                Id = 12,
                Nombre = "Samuel L. Jackson",
                FechaNacimmiento = new DateTime(1948, 12, 19),
                Fortuna = 500000
            };


            var scarletJohanson = new Actor()
            {
                Id = 13,
                Nombre = "Scarlet Johanson",
                FechaNacimmiento = new DateTime(1990, 01, 21),
                Fortuna = 600000
            };

            var roberDowney = new Actor()
            {
                Id = 14,
                Nombre = "Robert Downey Jr",
                FechaNacimmiento = new DateTime(1980, 11, 29),
                Fortuna = 7000000
            };

            modelBuilder.Entity<Actor>().HasData(samuelJackson, scarletJohanson, roberDowney);

            //Peliculas

            var avengers = new Pelicula()
            {
                Id = 3,
                Titulo = "Avengers Endgame",
                FechaEstreno = new DateTime(2021, 09, 13),
            };

            var ironMan = new Pelicula()
            {
                Id = 4,
                Titulo = "Iron Man",
                FechaEstreno = new DateTime(2010, 10, 10),
            };

            var capitanAmerica = new Pelicula()
            {
                Id = 5,
                Titulo = "Capitan America",
                FechaEstreno = new DateTime(2021, 07, 10),
            };

            modelBuilder.Entity<Pelicula>().HasData(avengers, ironMan, capitanAmerica);

            //Comentarios

            var fefita = new Comentario()
            {
                Id = 2,
                Contenido = "El mejor crossover y el primero grande marvel, dc no",
                Recomendar = true,
                PeliculaId = avengers.Id
            };

            var ramon = new Comentario()
            {
                Id = 3,
                Contenido = "El origen de todo",
                Recomendar = true,
                PeliculaId = ironMan.Id
            };

            var locoVida = new Comentario()
            {
                Id = 4,
                Contenido = "Viva el capi, pero no me gusto",
                Recomendar = false,
                PeliculaId = capitanAmerica.Id
            };

            modelBuilder.Entity<Comentario>().HasData(fefita, ramon, locoVida);

            //Parte un poco avanzada, relacion mucho a mucho con saltos, osea sin entidad intermedia.

            var tablaGeneroPelicula = "GeneroPelicula";
            var generoIdPropiedad = "GenerosId";
            var peliculaIdPropiedad = "PeliculasId";

            var historia = 7;
            var animacion = 8;

            //Como no hay una entidad intermedia se utilzia un diccionarios para representar las tablas y columnas con los datos que se mandaran


            modelBuilder.Entity(tablaGeneroPelicula).HasData(
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = historia,
                    [peliculaIdPropiedad] = avengers.Id

                },

                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = animacion,
                    [peliculaIdPropiedad] = ironMan.Id

                },

                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = historia,
                    [peliculaIdPropiedad] = capitanAmerica.Id

                }




                );

            //Muchos a muchos pero sin salto

            var samuelJacksonAvengers = new PeliculaActor
            {
                ActorId = samuelJackson.Id,
                PeliculaId = avengers.Id,
                Orden = 2,
                Personaje = "Nick Fury"
            };

            var scarlettJohansonAvengers = new PeliculaActor
            {
                ActorId = scarletJohanson.Id,
                PeliculaId = avengers.Id,
                Orden = 3,
                Personaje = "Natasha Romanoff - Black Widow"
            };

            var robertDowneyAvengers = new PeliculaActor
            {
                ActorId = roberDowney.Id,
                PeliculaId = avengers.Id,
                Orden = 1,
                Personaje = "Tony Stark-IronMan"
            };

            modelBuilder.Entity<PeliculaActor>().HasData(samuelJacksonAvengers, scarlettJohansonAvengers, robertDowneyAvengers);


        }
    }
}

