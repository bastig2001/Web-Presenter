using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebPresenter
{
    public partial class WebPresenterContext : DbContext
    {
        public WebPresenterContext()
        {
        }

        public WebPresenterContext(DbContextOptions<WebPresenterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Presentations> Presentations { get; set; }
        public virtual DbSet<Presenter> Presenter { get; set; }
        public virtual DbSet<Slides> Slides { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=WebPresenter;Username=postgres;Password=wasistpassiert");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presentations>(entity =>
            {
                entity.ToTable("presentations");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Presenterid).HasColumnName("presenterid");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(256);

                entity.HasOne(d => d.Presenter)
                    .WithMany(p => p.Presentations)
                    .HasForeignKey(d => d.Presenterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("presentations_presenterid_fkey");
            });

            modelBuilder.Entity<Presenter>(entity =>
            {
                entity.ToTable("presenter");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(256)
                    .HasDefaultValueSql("'General Kenobi'::character varying");
            });

            modelBuilder.Entity<Slides>(entity =>
            {
                entity.ToTable("slides");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Presentation).HasColumnName("presentation");

                entity.Property(e => e.Seqnr).HasColumnName("seqnr");

                entity.HasOne(d => d.PresentationNavigation)
                    .WithMany(p => p.Slides)
                    .HasForeignKey(d => d.Presentation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("slides_presentation_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
