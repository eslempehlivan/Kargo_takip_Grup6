using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace kargoKuryeAPİ.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alicilar> Alicilars { get; set; }

    public virtual DbSet<Araclar> Araclars { get; set; }

    public virtual DbSet<GonderiDurumlari> GonderiDurumlaris { get; set; }

    public virtual DbSet<GonderiKalemleri> GonderiKalemleris { get; set; }

    public virtual DbSet<Gonderiler> Gonderilers { get; set; }

    public virtual DbSet<Iadeler> Iadelers { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Kuryeler> Kuryelers { get; set; }

    public virtual DbSet<Musteriler> Musterilers { get; set; }

    public virtual DbSet<Odemeler> Odemelers { get; set; }

    public virtual DbSet<Roller> Rollers { get; set; }

    public virtual DbSet<Subeler> Subelers { get; set; }

    public virtual DbSet<TakipOlaylari> TakipOlaylaris { get; set; }

    public virtual DbSet<Teslimatlar> Teslimatlars { get; set; }

    public virtual DbSet<VwAktifKuryeler> VwAktifKuryelers { get; set; }

    public virtual DbSet<VwGonderiDetaylari> VwGonderiDetaylaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=kargoKuryeYonetimi6;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alicilar>(entity =>
        {
            entity.HasKey(e => e.AliciId).HasName("PK__Alicilar__E859CA3AF1101D22");

            entity.ToTable("Alicilar");

            entity.Property(e => e.AliciId)
                .ValueGeneratedNever()
                .HasColumnName("AliciID");
            entity.Property(e => e.AdSoyad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Adres)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Eposta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sehir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ulke)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("Türkiye");
        });

        modelBuilder.Entity<Araclar>(entity =>
        {
            entity.HasKey(e => e.AracId).HasName("PK__Araclar__1E09A8308938BE1F");

            entity.ToTable("Araclar");

            entity.HasIndex(e => e.AracPlaka, "UQ__Araclar__B41F794C8B415090").IsUnique();

            entity.Property(e => e.AracId)
                .ValueGeneratedNever()
                .HasColumnName("AracID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.AracPlaka)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AracTipi)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.KapasiteKg).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<GonderiDurumlari>(entity =>
        {
            entity.HasKey(e => e.DurumKodu).HasName("PK__GonderiD__FE331A6A41BF48F7");

            entity.ToTable("GonderiDurumlari");

            entity.Property(e => e.DurumKodu)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Aciklama)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GonderiKalemleri>(entity =>
        {
            entity.HasKey(e => e.KalemId).HasName("PK__GonderiK__5A77B0DDF6ACCE94");

            entity.ToTable("GonderiKalemleri");

            entity.Property(e => e.KalemId)
                .ValueGeneratedNever()
                .HasColumnName("KalemID");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Adet).HasDefaultValue(1);
            entity.Property(e => e.AgirlikKg).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.BoyutCm)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Desi).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.GonderiId).HasColumnName("GonderiID");
            entity.Property(e => e.UrunKodu)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Gonderi).WithMany(p => p.GonderiKalemleris)
                .HasForeignKey(d => d.GonderiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GonderiKa__Gonde__66603565");
        });

        modelBuilder.Entity<Gonderiler>(entity =>
        {
            entity.HasKey(e => e.GonderiId).HasName("PK__Gonderil__55EAD90F7AB71A4C");

            entity.ToTable("Gonderiler", tb =>
                {
                    tb.HasTrigger("trg_GonderiOlusturuldu");
                    tb.HasTrigger("trg_TeslimEdildiTarihiGuncelle");
                });

            entity.HasIndex(e => e.TakipNo, "UQ__Gonderil__B199EFBD7FA59FB1").IsUnique();

            entity.Property(e => e.GonderiId)
                .ValueGeneratedNever()
                .HasColumnName("GonderiID");
            entity.Property(e => e.AliciId).HasColumnName("AliciID");
            entity.Property(e => e.CikisSubeId).HasColumnName("CikisSubeID");
            entity.Property(e => e.Durum)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.GonderiTipi)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Standart");
            entity.Property(e => e.KuryeId).HasColumnName("KuryeID");
            entity.Property(e => e.MusteriId).HasColumnName("MusteriID");
            entity.Property(e => e.OdemeDurumu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Bekliyor");
            entity.Property(e => e.OlusturmaTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TahminiTeslimTarihi).HasColumnType("datetime");
            entity.Property(e => e.TakipNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeslimAlan)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TeslimTarihi).HasColumnType("datetime");
            entity.Property(e => e.ToplamAgirlikKg).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ToplamDesi).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.VarisSubeId).HasColumnName("VarisSubeID");

            entity.HasOne(d => d.Alici).WithMany(p => p.Gonderilers)
                .HasForeignKey(d => d.AliciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gonderile__Alici__5BE2A6F2");

            entity.HasOne(d => d.CikisSube).WithMany(p => p.GonderilerCikisSubes)
                .HasForeignKey(d => d.CikisSubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gonderile__Cikis__5CD6CB2B");

            entity.HasOne(d => d.DurumNavigation).WithMany(p => p.Gonderilers)
                .HasForeignKey(d => d.Durum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gonderile__Durum__5FB337D6");

            entity.HasOne(d => d.Kurye).WithMany(p => p.Gonderilers)
                .HasForeignKey(d => d.KuryeId)
                .HasConstraintName("FK__Gonderile__Kurye__5EBF139D");

            entity.HasOne(d => d.Musteri).WithMany(p => p.Gonderilers)
                .HasForeignKey(d => d.MusteriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gonderile__Muste__5AEE82B9");

            entity.HasOne(d => d.VarisSube).WithMany(p => p.GonderilerVarisSubes)
                .HasForeignKey(d => d.VarisSubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gonderile__Varis__5DCAEF64");
        });

        modelBuilder.Entity<Iadeler>(entity =>
        {
            entity.HasKey(e => e.IadeId).HasName("PK__Iadeler__D047997F3002C13A");

            entity.ToTable("Iadeler");

            entity.HasIndex(e => e.GonderiId, "UQ__Iadeler__55EAD90EA976B864").IsUnique();

            entity.Property(e => e.IadeId)
                .ValueGeneratedNever()
                .HasColumnName("IadeID");
            entity.Property(e => e.GonderiId).HasColumnName("GonderiID");
            entity.Property(e => e.IadeDurumu)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("İşleniyor");
            entity.Property(e => e.IadeNedeni)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IadeTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Gonderi).WithOne(p => p.Iadeler)
                .HasForeignKey<Iadeler>(d => d.GonderiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Iadeler__Gonderi__7D439ABD");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId).HasName("PK__Kullanic__E011F09BBEEF5A48");

            entity.ToTable("Kullanicilar");

            entity.HasIndex(e => e.KullaniciAdi, "UQ__Kullanic__5BAE6A756D1D7ADF").IsUnique();

            entity.Property(e => e.KullaniciId)
                .ValueGeneratedNever()
                .HasColumnName("KullaniciID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.SifreHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SonGirisTarihi).HasColumnType("datetime");

            entity.HasOne(d => d.Rol).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Kullanici__RolID__3C69FB99");
        });

        modelBuilder.Entity<Kuryeler>(entity =>
        {
            entity.HasKey(e => e.KuryeId).HasName("PK__Kuryeler__1BFD99DF569AB2C9");

            entity.ToTable("Kuryeler");

            entity.Property(e => e.KuryeId)
                .ValueGeneratedNever()
                .HasColumnName("KuryeID");
            entity.Property(e => e.AdSoyad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AracId).HasColumnName("AracID");
            entity.Property(e => e.Durum)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Müsait");
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.SubeId).HasColumnName("SubeID");
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Arac).WithMany(p => p.Kuryelers)
                .HasForeignKey(d => d.AracId)
                .HasConstraintName("FK__Kuryeler__AracID__4D94879B");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Kuryelers)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("FK__Kuryeler__Kullan__4E88ABD4");

            entity.HasOne(d => d.Sube).WithMany(p => p.Kuryelers)
                .HasForeignKey(d => d.SubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Kuryeler__SubeID__4CA06362");
        });

        modelBuilder.Entity<Musteriler>(entity =>
        {
            entity.HasKey(e => e.MusteriId).HasName("PK__Musteril__726244711ED38796");

            entity.ToTable("Musteriler");

            entity.HasIndex(e => e.Eposta, "UQ__Musteril__03ABA3912E4CF52F").IsUnique();

            entity.Property(e => e.MusteriId)
                .ValueGeneratedNever()
                .HasColumnName("MusteriID");
            entity.Property(e => e.AdSoyad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Adres)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Eposta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.KayitTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.Sehir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ulke)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("Türkiye");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Musterilers)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("FK__Musterile__Kulla__4222D4EF");
        });

        modelBuilder.Entity<Odemeler>(entity =>
        {
            entity.HasKey(e => e.OdemeId).HasName("PK__Odemeler__B11B66AD961D65E7");

            entity.ToTable("Odemeler");

            entity.Property(e => e.OdemeId)
                .ValueGeneratedNever()
                .HasColumnName("OdemeID");
            entity.Property(e => e.GonderiId).HasColumnName("GonderiID");
            entity.Property(e => e.OdemeDurumu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Başarılı");
            entity.Property(e => e.OdemeTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ParaBirimi)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("TRY");
            entity.Property(e => e.Tutar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Yontem)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Gonderi).WithMany(p => p.Odemelers)
                .HasForeignKey(d => d.GonderiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Odemeler__Gonder__71D1E811");
        });

        modelBuilder.Entity<Roller>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roller__F92302D17A443248");

            entity.ToTable("Roller");

            entity.HasIndex(e => e.RolAdi, "UQ__Roller__85F2635D2B9DAF0B").IsUnique();

            entity.Property(e => e.RolId)
                .ValueGeneratedNever()
                .HasColumnName("RolID");
            entity.Property(e => e.RolAdi)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subeler>(entity =>
        {
            entity.HasKey(e => e.SubeId).HasName("PK__Subeler__C3041019A8289D1E");

            entity.ToTable("Subeler");

            entity.Property(e => e.SubeId)
                .ValueGeneratedNever()
                .HasColumnName("SubeID");
            entity.Property(e => e.AcikMi).HasDefaultValue(true);
            entity.Property(e => e.Adres)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Sehir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubeAdi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TakipOlaylari>(entity =>
        {
            entity.HasKey(e => e.OlayId).HasName("PK__TakipOla__F7A439A79172FC76");

            entity.ToTable("TakipOlaylari");

            entity.Property(e => e.OlayId)
                .ValueGeneratedNever()
                .HasColumnName("OlayID");
            entity.Property(e => e.Durum)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.GonderiId).HasColumnName("GonderiID");
            entity.Property(e => e.IslemYapanKullaniciId).HasColumnName("IslemYapanKullaniciID");
            entity.Property(e => e.Konum)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Notlar)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OlayZamani)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DurumNavigation).WithMany(p => p.TakipOlaylaris)
                .HasForeignKey(d => d.Durum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TakipOlay__Durum__6B24EA82");

            entity.HasOne(d => d.Gonderi).WithMany(p => p.TakipOlaylaris)
                .HasForeignKey(d => d.GonderiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TakipOlay__Gonde__6A30C649");

            entity.HasOne(d => d.IslemYapanKullanici).WithMany(p => p.TakipOlaylaris)
                .HasForeignKey(d => d.IslemYapanKullaniciId)
                .HasConstraintName("FK__TakipOlay__Islem__6C190EBB");
        });

        modelBuilder.Entity<Teslimatlar>(entity =>
        {
            entity.HasKey(e => e.TeslimatId).HasName("PK__Teslimat__031BAA9F3BCFF822");

            entity.ToTable("Teslimatlar");

            entity.HasIndex(e => e.GonderiId, "UQ__Teslimat__55EAD90E72CBA651").IsUnique();

            entity.Property(e => e.TeslimatId)
                .ValueGeneratedNever()
                .HasColumnName("TeslimatID");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GonderiId).HasColumnName("GonderiID");
            entity.Property(e => e.TeslimAlanAdSoyad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TeslimAlanKimlikNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeslimatTarihi).HasColumnType("datetime");

            entity.HasOne(d => d.Gonderi).WithOne(p => p.Teslimatlar)
                .HasForeignKey<Teslimatlar>(d => d.GonderiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teslimatl__Gonde__778AC167");
        });

        modelBuilder.Entity<VwAktifKuryeler>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_AktifKuryeler");

            entity.Property(e => e.AdSoyad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AracPlaka)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Durum)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.KuryeId).HasColumnName("KuryeID");
            entity.Property(e => e.SubeAdi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwGonderiDetaylari>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_GonderiDetaylari");

            entity.Property(e => e.Alici)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CikisSube)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Durum)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Gonderen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GonderiId).HasColumnName("GonderiID");
            entity.Property(e => e.GonderiTipi)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Kurye)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OdemeDurumu)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OlusturmaTarihi).HasColumnType("datetime");
            entity.Property(e => e.TahminiTeslimTarihi).HasColumnType("datetime");
            entity.Property(e => e.TakipNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeslimTarihi).HasColumnType("datetime");
            entity.Property(e => e.VarisSube)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
