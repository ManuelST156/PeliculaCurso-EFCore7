﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeliculaEntity;

#nullable disable

namespace PeliculaEntity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeneroPelicula", b =>
                {
                    b.Property<int>("GenerosId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculasId")
                        .HasColumnType("int");

                    b.HasKey("GenerosId", "PeliculasId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("GeneroPelicula");

                    b.HasData(
                        new
                        {
                            GenerosId = 7,
                            PeliculasId = 3
                        },
                        new
                        {
                            GenerosId = 8,
                            PeliculasId = 4
                        },
                        new
                        {
                            GenerosId = 7,
                            PeliculasId = 5
                        });
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaNacimmiento")
                        .HasColumnType("date");

                    b.Property<decimal>("Fortuna")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Actores");

                    b.HasData(
                        new
                        {
                            Id = 12,
                            FechaNacimmiento = new DateTime(1948, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Fortuna = 500000m,
                            Nombre = "Samuel L. Jackson"
                        },
                        new
                        {
                            Id = 13,
                            FechaNacimmiento = new DateTime(1990, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Fortuna = 600000m,
                            Nombre = "Scarlet Johanson"
                        },
                        new
                        {
                            Id = 14,
                            FechaNacimmiento = new DateTime(1980, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Fortuna = 7000000m,
                            Nombre = "Robert Downey Jr"
                        });
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<bool>("Recomendar")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PeliculaId");

                    b.ToTable("Comentarios");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Contenido = "El mejor crossover y el primero grande marvel, dc no",
                            PeliculaId = 3,
                            Recomendar = true
                        },
                        new
                        {
                            Id = 3,
                            Contenido = "El origen de todo",
                            PeliculaId = 4,
                            Recomendar = true
                        },
                        new
                        {
                            Id = 4,
                            Contenido = "Viva el capi, pero no me gusto",
                            PeliculaId = 5,
                            Recomendar = false
                        });
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 7,
                            Nombre = "Historia"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Animacion"
                        });
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EnCines")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("date");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            EnCines = false,
                            FechaEstreno = new DateTime(2021, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers Endgame"
                        },
                        new
                        {
                            Id = 4,
                            EnCines = false,
                            FechaEstreno = new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Iron Man"
                        },
                        new
                        {
                            Id = 5,
                            EnCines = false,
                            FechaEstreno = new DateTime(2021, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Capitan America"
                        });
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.PeliculaActor", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("ActorId", "PeliculaId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculasActores");

                    b.HasData(
                        new
                        {
                            ActorId = 12,
                            PeliculaId = 3,
                            Orden = 2,
                            Personaje = "Nick Fury"
                        },
                        new
                        {
                            ActorId = 13,
                            PeliculaId = 3,
                            Orden = 3,
                            Personaje = "Natasha Romanoff - Black Widow"
                        },
                        new
                        {
                            ActorId = 14,
                            PeliculaId = 3,
                            Orden = 1,
                            Personaje = "Tony Stark-IronMan"
                        });
                });

            modelBuilder.Entity("GeneroPelicula", b =>
                {
                    b.HasOne("PeliculaEntity.Entidades.Genero", null)
                        .WithMany()
                        .HasForeignKey("GenerosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculaEntity.Entidades.Pelicula", null)
                        .WithMany()
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Comentario", b =>
                {
                    b.HasOne("PeliculaEntity.Entidades.Pelicula", "Pelicula")
                        .WithMany("Comentarios")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.PeliculaActor", b =>
                {
                    b.HasOne("PeliculaEntity.Entidades.Actor", "Actor")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculaEntity.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Actor", b =>
                {
                    b.Navigation("PeliculasActores");
                });

            modelBuilder.Entity("PeliculaEntity.Entidades.Pelicula", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("PeliculasActores");
                });
#pragma warning restore 612, 618
        }
    }
}
