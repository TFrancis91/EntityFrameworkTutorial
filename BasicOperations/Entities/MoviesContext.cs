using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BasicOperations.Entities
{
    public partial class MoviesContext : DbContext
    {
        private static MoviesContext _context;
        public static MoviesContext Instance
        {
            get
            {
                if (_context == null)
                {
                    _context = new MoviesContext();
                }
                return _context;
            }
        }
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmActor> FilmActor { get; set; }
        public virtual DbSet<FilmCategory> FilmCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build().GetConnectionString("DbConnection");

                optionsBuilder.UseMySql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("actor");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Name).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("film");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Rating).HasColumnType("varchar(45)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.ActorId })
                    .HasName("PRIMARY");

                entity.ToTable("film_actor");

                entity.HasIndex(e => e.ActorId)
                    .HasName("film_actor_actor_idx");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("film_actor_actor");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("film_actor_film");
            });

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.CategoryId })
                    .HasName("PRIMARY");

                entity.ToTable("film_category");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("film_category_category_fk_idx");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FilmCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("film_category_category_fk");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmCategory)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("film_category_film_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
