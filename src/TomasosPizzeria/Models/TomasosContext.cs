using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TomasosPizzeria.Models
{
    public partial class TomasosContext : DbContext
    {
        public virtual DbSet<Bestallning> Bestallning { get; set; }
        public virtual DbSet<BestallningMatratt> BestallningMatratt { get; set; }
        public virtual DbSet<Korg> Korg { get; set; }
        public virtual DbSet<Kund> Kund { get; set; }
        public virtual DbSet<Matratt> Matratt { get; set; }
        public virtual DbSet<MatrattTyp> MatrattTyp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Tomasos;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bestallning>(entity =>
            {
                entity.Property(e => e.BestallningId).HasColumnName("BestallningID");

                entity.Property(e => e.BestallningDatum).HasColumnType("datetime");

                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.HasOne(d => d.Kund)
                    .WithMany(p => p.Bestallning)
                    .HasForeignKey(d => d.KundId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bestallning_Kund");
            });

            modelBuilder.Entity<BestallningMatratt>(entity =>
            {
                entity.HasKey(e => new { e.MatrattId, e.BestallningId })
                    .HasName("PK_BestallningMatratt");

                entity.Property(e => e.MatrattId).HasColumnName("MatrattID");

                entity.Property(e => e.BestallningId).HasColumnName("BestallningID");

                entity.HasOne(d => d.Bestallning)
                    .WithMany(p => p.BestallningMatratt)
                    .HasForeignKey(d => d.BestallningId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_BestallningMatratt_Bestallning");

                entity.HasOne(d => d.Matratt)
                    .WithMany(p => p.BestallningMatratt)
                    .HasForeignKey(d => d.MatrattId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_BestallningMatratt_Matratt");
            });

            modelBuilder.Entity<Korg>(entity =>
            {
                entity.Property(e => e.KorgId).HasColumnName("KorgID");

                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.Property(e => e.Matrattnamn).HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<Kund>(entity =>
            {
                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.Property(e => e.AnvandarNamn)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.Gatuadress)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Losenord)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Namn)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Postnr)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Postort)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Telefon).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Matratt>(entity =>
            {
                entity.Property(e => e.MatrattId).HasColumnName("MatrattID");

                entity.Property(e => e.Beskrivning).HasColumnType("varchar(200)");

                entity.Property(e => e.MatrattNamn)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.MatrattTypNavigation)
                    .WithMany(p => p.Matratt)
                    .HasForeignKey(d => d.MatrattTyp)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Matratt_MatrattTyp");
            });

            modelBuilder.Entity<MatrattTyp>(entity =>
            {
                entity.HasKey(e => e.MatrattTyp1)
                    .HasName("PK_MatrattTyp");

                entity.Property(e => e.MatrattTyp1).HasColumnName("MatrattTyp");

                entity.Property(e => e.Beskrivning)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }
    }
}