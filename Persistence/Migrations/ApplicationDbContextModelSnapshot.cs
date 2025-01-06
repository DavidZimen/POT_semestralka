﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("application_schema")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entity.ActorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("actor", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.DirectorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("director", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.EpisodeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("DirectorId")
                        .HasColumnType("uuid")
                        .HasColumnName("director_id");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("release_data");

                    b.Property<Guid>("SeasonId")
                        .HasColumnType("uuid")
                        .HasColumnName("season_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.HasIndex("SeasonId");

                    b.ToTable("episode", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.FilmEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("DirectorId")
                        .HasColumnType("uuid")
                        .HasColumnName("director_id");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("release_data");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("film", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.GenreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("UQ_Genre_Name");

                    b.ToTable("genre", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Bio")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("bio");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("char(2)")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("middle_name");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.HasKey("Id");

                    b.ToTable("person", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.RatingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid?>("EpisodeId")
                        .HasColumnType("uuid")
                        .HasColumnName("episode_id");

                    b.Property<Guid?>("FilmId")
                        .HasColumnType("uuid")
                        .HasColumnName("film_id");

                    b.Property<Guid?>("ShowId")
                        .HasColumnType("uuid")
                        .HasColumnName("show_id");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasColumnName("user_id");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("FilmId");

                    b.HasIndex("ShowId");

                    b.HasIndex("UserId");

                    b.ToTable("rating", "application_schema", t =>
                        {
                            t.HasCheckConstraint("CHK_One_Type_Not_Null", "\"film_id\" IS NOT NULL OR \"show_id\" IS NOT NULL OR \"episode_id\" IS NOT NULL");

                            t.HasCheckConstraint("CHK_Rating_value", "\"value\" >= 1 AND \"value\" <= 10");
                        });
                });

            modelBuilder.Entity("Domain.Entity.SeasonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<Guid>("ShowId")
                        .HasColumnType("uuid")
                        .HasColumnName("show_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.ToTable("season", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.ShowEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("show", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean")
                        .HasColumnName("enabled");

                    b.HasKey("Id");

                    b.ToTable("user", "application_schema");
                });

            modelBuilder.Entity("episode_actor", b =>
                {
                    b.Property<Guid>("episode_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("actor_id")
                        .HasColumnType("uuid");

                    b.HasKey("episode_id", "actor_id");

                    b.HasIndex("actor_id");

                    b.ToTable("episode_actor", "application_schema");
                });

            modelBuilder.Entity("film_actor", b =>
                {
                    b.Property<Guid>("film_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("actor_id")
                        .HasColumnType("uuid");

                    b.HasKey("film_id", "actor_id");

                    b.HasIndex("actor_id");

                    b.ToTable("film_actor", "application_schema");
                });

            modelBuilder.Entity("film_genre", b =>
                {
                    b.Property<Guid>("genre_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("film_id")
                        .HasColumnType("uuid");

                    b.HasKey("genre_id", "film_id");

                    b.HasIndex("film_id");

                    b.ToTable("film_genre", "application_schema");
                });

            modelBuilder.Entity("show_genre", b =>
                {
                    b.Property<Guid>("genre_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("show_id")
                        .HasColumnType("uuid");

                    b.HasKey("genre_id", "show_id");

                    b.HasIndex("show_id");

                    b.ToTable("show_genre", "application_schema");
                });

            modelBuilder.Entity("Domain.Entity.ActorEntity", b =>
                {
                    b.HasOne("Domain.Entity.PersonEntity", "Person")
                        .WithOne("Actor")
                        .HasForeignKey("Domain.Entity.ActorEntity", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.Entity.DirectorEntity", b =>
                {
                    b.HasOne("Domain.Entity.PersonEntity", "Person")
                        .WithOne("Director")
                        .HasForeignKey("Domain.Entity.DirectorEntity", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.Entity.EpisodeEntity", b =>
                {
                    b.HasOne("Domain.Entity.DirectorEntity", "Director")
                        .WithMany("Episodes")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.SeasonEntity", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("Domain.Entity.FilmEntity", b =>
                {
                    b.HasOne("Domain.Entity.DirectorEntity", "Director")
                        .WithMany("Films")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("Domain.Entity.RatingEntity", b =>
                {
                    b.HasOne("Domain.Entity.EpisodeEntity", "Episode")
                        .WithMany("Ratings")
                        .HasForeignKey("EpisodeId");

                    b.HasOne("Domain.Entity.FilmEntity", "Film")
                        .WithMany("Ratings")
                        .HasForeignKey("FilmId");

                    b.HasOne("Domain.Entity.ShowEntity", "Show")
                        .WithMany("Ratings")
                        .HasForeignKey("ShowId");

                    b.HasOne("Domain.Entity.UserEntity", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Episode");

                    b.Navigation("Film");

                    b.Navigation("Show");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.SeasonEntity", b =>
                {
                    b.HasOne("Domain.Entity.ShowEntity", "Show")
                        .WithMany("Seasons")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Show");
                });

            modelBuilder.Entity("episode_actor", b =>
                {
                    b.HasOne("Domain.Entity.ActorEntity", null)
                        .WithMany()
                        .HasForeignKey("actor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.EpisodeEntity", null)
                        .WithMany()
                        .HasForeignKey("episode_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("film_actor", b =>
                {
                    b.HasOne("Domain.Entity.ActorEntity", null)
                        .WithMany()
                        .HasForeignKey("actor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.FilmEntity", null)
                        .WithMany()
                        .HasForeignKey("film_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("film_genre", b =>
                {
                    b.HasOne("Domain.Entity.FilmEntity", null)
                        .WithMany()
                        .HasForeignKey("film_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.GenreEntity", null)
                        .WithMany()
                        .HasForeignKey("genre_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("show_genre", b =>
                {
                    b.HasOne("Domain.Entity.GenreEntity", null)
                        .WithMany()
                        .HasForeignKey("genre_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.ShowEntity", null)
                        .WithMany()
                        .HasForeignKey("show_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entity.DirectorEntity", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("Films");
                });

            modelBuilder.Entity("Domain.Entity.EpisodeEntity", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Domain.Entity.FilmEntity", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Domain.Entity.PersonEntity", b =>
                {
                    b.Navigation("Actor");

                    b.Navigation("Director");
                });

            modelBuilder.Entity("Domain.Entity.SeasonEntity", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("Domain.Entity.ShowEntity", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Seasons");
                });

            modelBuilder.Entity("Domain.Entity.UserEntity", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
