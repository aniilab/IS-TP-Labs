using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieWebApplication
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
        public virtual DbSet<MovieInPlaylist> MovieInPlaylists { get; set; } = null!;
        public virtual DbSet<MoviesGenre> MoviesGenres { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<Production> Productions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= ALINA; Database=MovieDB; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArtistName)
                    .IsUnicode(false)
                    .HasColumnName("artist_name");

                entity.Property(e => e.Oscars).HasColumnName("oscars");
            });

            modelBuilder.Entity<ArtistsJobsInMovie>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("job");

                entity.Property(e => e.MovieId).HasColumnName("movieID");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistsJobsInMovies)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistsJobsInMovies_Artist");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ArtistsJobsInMovies)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_ArtistsJobsInMovies_Movie");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GenreName)
                    .IsUnicode(false)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.HasOscar).HasColumnName("has_oscar");

                entity.Property(e => e.MovieDuration).HasColumnName("movie_duration");

                entity.Property(e => e.MovieName).HasColumnName("movie_name");

                entity.Property(e => e.ProductionId).HasColumnName("productionID");

                entity.HasOne(d => d.Production)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.ProductionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_Production");
            });

            modelBuilder.Entity<MovieInPlaylist>(entity =>
            {
                entity.ToTable("MovieInPlaylist");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MovieId).HasColumnName("movieID");

                entity.Property(e => e.PlaylistId).HasColumnName("playlistID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieInPlaylists)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_MovieInPlaylist_Movie");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.MovieInPlaylists)
                    .HasForeignKey(d => d.PlaylistId)
                    .HasConstraintName("FK_MovieInPlaylist_Playlist");
            });

            modelBuilder.Entity<MoviesGenre>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GenreId).HasColumnName("genreID");

                entity.Property(e => e.MovieId).HasColumnName("movieID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MoviesGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesGenres_Genre");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesGenres)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_MoviesGenres_Movie");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PlaylistName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("playlist_name");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Playlists_User");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.ToTable("Production");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MovieId).HasColumnName("movieID");

                entity.Property(e => e.ProdCountry)
                    .IsUnicode(false)
                    .HasColumnName("prod_country");

                entity.Property(e => e.ProdName)
                    .IsUnicode(false)
                    .HasColumnName("prod_name");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_Production_Movie");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlaylistId).HasColumnName("playlistID");

                entity.Property(e => e.UserEmail)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserInfo)
                    .IsUnicode(false)
                    .HasColumnName("user_info");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PlaylistId)
                    .HasConstraintName("FK_Users_Playlist");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
