using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoviesWebApplication
{
    public partial class MovieDBContext : DbContext
    {
        public MovieDBContext()
        {
        }

        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<ArtistsJobsInMovie> ArtistsJobsInMovies { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MoviesGenre> MoviesGenres { get; set; } = null!;
        public virtual DbSet<MoviesInPlaylist> MoviesInPlaylists { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<Production> Productions { get; set; } = null!;
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= ALINA; Database= MovieDB; Trusted_Connection= True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ArtistsJobsInMovie>(entity =>
            {
                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistsJobsInMovies)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ArtistsJobsInMovies_Artist");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ArtistsJobsInMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ArtistsJobsInMovies_Movie");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.HasOne(d => d.Production)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.ProductionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Movie_Production");
            });

            modelBuilder.Entity<MoviesGenre>(entity =>
            {
                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MoviesGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MoviesGenres_Genre");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MoviesGenres_Movie");
            });

            modelBuilder.Entity<MoviesInPlaylist>(entity =>
            {
                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesInPlaylists)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MovieInPlaylist_Movie");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.MoviesInPlaylists)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MovieInPlaylist_Playlist");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId);
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
