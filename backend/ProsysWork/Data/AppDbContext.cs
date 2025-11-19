using Microsoft.EntityFrameworkCore;
using ProsysWork.Models;

namespace ProsysWork.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Shagird> Shagirdler { get; set; }
        public DbSet<Imtahan> Imtahanlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ders>(entity =>
            {
                entity.HasKey(e => e.DersKodu);
                entity.Property(e => e.DersKodu)
                    .HasColumnType("char(3)")
                    .HasMaxLength(3)
                    .IsRequired();
                entity.Property(e => e.DersAdi)
                    .HasColumnType("varchar(30)")
                    .HasMaxLength(30)
                    .IsRequired();
                entity.Property(e => e.Sinifi)
                    .HasColumnType("smallint")
                    .IsRequired();
                entity.Property(e => e.MuellimAdi)
                    .HasColumnType("varchar(20)")
                    .HasMaxLength(20)
                    .IsRequired();
                entity.Property(e => e.MuellimSoyadi)
                    .HasColumnType("varchar(20)")
                    .HasMaxLength(20)
                    .IsRequired();
            });

            modelBuilder.Entity<Shagird>(entity =>
            {
                entity.HasKey(e => e.Nomresi);
                entity.Property(e => e.Nomresi)
                    .HasColumnType("int")
                    .ValueGeneratedNever()
                    .IsRequired();
                entity.Property(e => e.Adi)
                    .HasColumnType("varchar(30)")
                    .HasMaxLength(30)
                    .IsRequired();
                entity.Property(e => e.Soyadi)
                    .HasColumnType("varchar(30)")
                    .HasMaxLength(30)
                    .IsRequired();
                entity.Property(e => e.Sinifi)
                    .HasColumnType("smallint")
                    .IsRequired();
            });

            modelBuilder.Entity<Imtahan>(entity =>
            {
                entity.HasKey(e => new { e.DersKodu, e.ShagirdNomresi, e.ImtahanTarixi });
                entity.Property(e => e.DersKodu)
                    .HasColumnType("char(3)")
                    .HasMaxLength(3)
                    .IsRequired();
                entity.Property(e => e.ShagirdNomresi)
                    .HasColumnType("int")
                    .IsRequired();
                entity.Property(e => e.ImtahanTarixi)
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(e => e.Qiymeti)
                    .HasColumnType("tinyint")
                    .IsRequired();

                entity.HasOne(d => d.Ders)
                    .WithMany(p => p.Imtahanlar)
                    .HasForeignKey(d => d.DersKodu)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Shagird)
                    .WithMany(p => p.Imtahanlar)
                    .HasForeignKey(d => d.ShagirdNomresi)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

