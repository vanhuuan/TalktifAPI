using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TalktifAPI.Models
{
    public partial class TalktifContext : DbContext
    {
        public TalktifContext()
        {
        }

        public TalktifContext(DbContextOptions<TalktifContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatRoom> ChatRooms { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserChatRoom> UserChatRooms { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TPTMLAN; Initial Catalog=Talktif;User ID=Talktif; Password=vanhuuan89");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.Property(e => e.ChatRoomName).IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__City__countryId__7849DB76");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__chatRoo__2334397B");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(d => d.ReporterNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Reporter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__2610A626");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ConfirmedEmail).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Gender).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((1))");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__cityId__14E61A24");
            });

            modelBuilder.Entity<UserChatRoom>(entity =>
            {
                entity.HasKey(e => new { e.User, e.ChatRoomId })
                    .HasName("PK__User_Cha__4372E63ACB17237C");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.UserChatRooms)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Chat__chatR__2057CCD0");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserChatRooms)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_ChatR__user__1F63A897");
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.Property(e => e.Device).IsUnicode(false);

                entity.Property(e => e.RefreshToken).IsUnicode(false);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserRefreshTokens)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Refre__user__1A9EF37A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
