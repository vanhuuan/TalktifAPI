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
                optionsBuilder.UseSqlServer("Server=talktif.database.windows.net; Initial Catalog=TalktifDB;User ID=talktif; Password=19tclcdt3@");
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
                    .HasConstraintName("FK__City__countryId__17F790F9");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__chatRoo__7755B73D");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(d => d.ReporterNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Reporter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__7A3223E8");
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
                    .HasConstraintName("FK__User__cityId__690797E6");
            });

            modelBuilder.Entity<UserChatRoom>(entity =>
            {
                entity.HasKey(e => new { e.User, e.ChatRoomId })
                    .HasName("PK__User_Cha__4372E63A22407D25");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.UserChatRooms)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Chat__chatR__74794A92");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserChatRooms)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_ChatR__user__73852659");
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.Property(e => e.Device).IsUnicode(false);

                entity.Property(e => e.RefreshToken).IsUnicode(false);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserRefreshTokens)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Refre__user__6EC0713C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
