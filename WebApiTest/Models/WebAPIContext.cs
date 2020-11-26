using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiTest.Models
{
    public partial class WebAPIContext : DbContext
    {
        public WebAPIContext()
        {
        }

        public WebAPIContext(DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drejtimet> Drejtimets { get; set; }
        public virtual DbSet<Lendet> Lendets { get; set; }
        public virtual DbSet<Profesoret> Profesorets { get; set; }
        public virtual DbSet<Provimet> Provimets { get; set; }
        public virtual DbSet<ProvimetStudenteve> ProvimetStudenteves { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Statuset> Statusets { get; set; }
        public virtual DbSet<Studentet> Studentets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=WebApiDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drejtimet>(entity =>
            {
                entity.ToTable("Drejtimet");

                entity.HasIndex(e => e.Emri, "UQ__Drejtime__DCB4757FB6DFD9B0")
                    .IsUnique();

                entity.Property(e => e.Emri)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Komenti).IsUnicode(false);
            });

            modelBuilder.Entity<Lendet>(entity =>
            {
                entity.ToTable("Lendet");

                entity.Property(e => e.Emri).IsUnicode(false);

                entity.HasOne(d => d.Drejtimi)
                    .WithMany(p => p.Lendets)
                    .HasForeignKey(d => d.DrejtimiId)
                    .HasConstraintName("FK__Lendet__Drejtimi__3E52440B");

                entity.HasOne(d => d.Profesori)
                    .WithMany(p => p.Lendets)
                    .HasForeignKey(d => d.ProfesoriId)
                    .HasConstraintName("FK__Lendet__Profesor__66603565");
            });

            modelBuilder.Entity<Profesoret>(entity =>
            {
                entity.ToTable("Profesoret");

                entity.Property(e => e.Emri).IsUnicode(false);

                entity.Property(e => e.Mbiemri).IsUnicode(false);
            });

            modelBuilder.Entity<Provimet>(entity =>
            {
                entity.ToTable("Provimet");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.HasOne(d => d.Lenda)
                    .WithMany(p => p.Provimets)
                    .HasForeignKey(d => d.LendaId)
                    .HasConstraintName("FK__Provimet__LendaI__6C190EBB");
            });

            modelBuilder.Entity<ProvimetStudenteve>(entity =>
            {
                entity.ToTable("ProvimetStudenteve");

                entity.HasOne(d => d.Provim)
                    .WithMany(p => p.ProvimetStudenteves)
                    .HasForeignKey(d => d.ProvimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProvimetS__Provi__6FE99F9F");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ProvimetStudenteves)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProvimetS__Stude__6EF57B66");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Statuset>(entity =>
            {
                entity.ToTable("Statuset");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Studentet>(entity =>
            {
                entity.ToTable("Studentet");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataLindjes).HasColumnType("datetime");

                entity.Property(e => e.Emri).IsUnicode(false);

                entity.Property(e => e.Mbiemri).IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Drejtimi)
                    .WithMany(p => p.Studentets)
                    .HasForeignKey(d => d.DrejtimiId)
                    .HasConstraintName("FK__Studentet__Drejt__4E88ABD4");

                entity.HasOne(d => d.Statusi)
                    .WithMany(p => p.Studentets)
                    .HasForeignKey(d => d.StatusiId)
                    .HasConstraintName("FK__Studentet__Statu__4F7CD00D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
