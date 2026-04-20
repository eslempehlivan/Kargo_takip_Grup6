CREATE DATABASE kargoKuryeYonetimi6;
GO
USE kargoKuryeYonetimi6;
GO

CREATE TABLE Roller
(
    RolID INT PRIMARY KEY,
    RolAdi VARCHAR(30) NOT NULL UNIQUE
);
GO

CREATE TABLE Kullanicilar
(
    KullaniciID INT PRIMARY KEY,
    KullaniciAdi VARCHAR(50) NOT NULL UNIQUE,
    SifreHash VARCHAR(255) NOT NULL,
    RolID INT NOT NULL,
    AktifMi BIT NOT NULL DEFAULT 1,
    SonGirisTarihi DATETIME NULL,
    FOREIGN KEY (RolID) REFERENCES Roller(RolID)
);
GO

CREATE TABLE Musteriler
(
    MusteriID INT PRIMARY KEY,
    AdSoyad VARCHAR(100) NOT NULL,
    Telefon VARCHAR(20) NOT NULL,
    Eposta VARCHAR(100) NOT NULL UNIQUE,
    Adres VARCHAR(200) NOT NULL,
    Sehir VARCHAR(100) NOT NULL,
    Ulke VARCHAR(100) NOT NULL DEFAULT 'Türkiye',
    KayitTarihi DATETIME NOT NULL DEFAULT GETDATE(),
    KullaniciID INT NULL,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID)
);
GO

CREATE TABLE Subeler
(
    SubeID INT PRIMARY KEY,
    SubeAdi VARCHAR(100) NOT NULL,
    Sehir VARCHAR(100) NOT NULL,
    Adres VARCHAR(200) NOT NULL,
    Telefon VARCHAR(20) NULL,
    AcikMi BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Araclar
(
    AracID INT PRIMARY KEY,
    AracPlaka VARCHAR(20) NOT NULL UNIQUE,
    AracTipi VARCHAR(30) NOT NULL,
    KapasiteKg DECIMAL(8,2) NULL,
    AktifMi BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Kuryeler
(
    KuryeID INT PRIMARY KEY,
    AdSoyad VARCHAR(100) NOT NULL,
    Telefon VARCHAR(20) NOT NULL,
    SubeID INT NOT NULL,
    AracID INT NULL,
    Durum VARCHAR(30) NOT NULL DEFAULT 'Müsait',
    IseBaslamaTarihi DATE NULL,
    KullaniciID INT NULL,
    FOREIGN KEY (SubeID) REFERENCES Subeler(SubeID),
    FOREIGN KEY (AracID) REFERENCES Araclar(AracID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID),
    CONSTRAINT CK_KuryeDurum CHECK (Durum IN ('Müsait', 'Daðýtýmda', 'Ýzinde', 'Pasif'))
);
GO

CREATE TABLE Alicilar
(
    AliciID INT PRIMARY KEY,
    AdSoyad VARCHAR(100) NOT NULL,
    Telefon VARCHAR(20) NOT NULL,
    Eposta VARCHAR(100) NULL,
    Adres VARCHAR(200) NOT NULL,
    Sehir VARCHAR(100) NOT NULL,
    Ulke VARCHAR(100) NOT NULL DEFAULT 'Türkiye'
);
GO

CREATE TABLE GonderiDurumlari
(
    DurumKodu VARCHAR(30) PRIMARY KEY,
    Aciklama VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Gonderiler
(
    GonderiID INT PRIMARY KEY,
    TakipNo VARCHAR(30) NOT NULL UNIQUE,
    MusteriID INT NOT NULL,
    AliciID INT NOT NULL,
    CikisSubeID INT NOT NULL,
    VarisSubeID INT NOT NULL,
    KuryeID INT NULL,
    Durum VARCHAR(30) NOT NULL,
    GonderiTipi VARCHAR(30) NOT NULL DEFAULT 'Standart',
    OdemeDurumu VARCHAR(20) NOT NULL DEFAULT 'Bekliyor',
    ToplamDesi DECIMAL(8,2) NULL,
    ToplamAgirlikKg DECIMAL(8,2) NULL,
    OlusturmaTarihi DATETIME NOT NULL DEFAULT GETDATE(),
    TahminiTeslimTarihi DATETIME NULL,
    TeslimTarihi DATETIME NULL,
    TeslimAlan VARCHAR(100) NULL,
    FOREIGN KEY (MusteriID) REFERENCES Musteriler(MusteriID),
    FOREIGN KEY (AliciID) REFERENCES Alicilar(AliciID),
    FOREIGN KEY (CikisSubeID) REFERENCES Subeler(SubeID),
    FOREIGN KEY (VarisSubeID) REFERENCES Subeler(SubeID),
    FOREIGN KEY (KuryeID) REFERENCES Kuryeler(KuryeID),
    FOREIGN KEY (Durum) REFERENCES GonderiDurumlari(DurumKodu),
    CONSTRAINT CK_GonderiTipi CHECK (GonderiTipi IN ('Standart', 'Ekspres', 'Ayný Gün')),
    CONSTRAINT CK_OdemeDurumu CHECK (OdemeDurumu IN ('Bekliyor', 'Ödendi', 'Ýade'))
);
GO

CREATE TABLE GonderiKalemleri
(
    KalemID INT PRIMARY KEY,
    GonderiID INT NOT NULL,
    UrunKodu VARCHAR(50) NOT NULL,
    Aciklama VARCHAR(200) NOT NULL,
    AgirlikKg DECIMAL(6,2) NOT NULL,
    BoyutCm VARCHAR(50) NULL,
    Desi DECIMAL(8,2) NULL,
    Adet INT NOT NULL DEFAULT 1,
    KýrýlabilirMi BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (GonderiID) REFERENCES Gonderiler(GonderiID)
);
GO

CREATE TABLE TakipOlaylari
(
    OlayID INT PRIMARY KEY,
    GonderiID INT NOT NULL,
    OlayZamani DATETIME NOT NULL DEFAULT GETDATE(),
    Konum VARCHAR(100) NOT NULL,
    Durum VARCHAR(30) NOT NULL,
    Notlar VARCHAR(200) NULL,
    IslemYapanKullaniciID INT NULL,
    FOREIGN KEY (GonderiID) REFERENCES Gonderiler(GonderiID),
    FOREIGN KEY (Durum) REFERENCES GonderiDurumlari(DurumKodu),
    FOREIGN KEY (IslemYapanKullaniciID) REFERENCES Kullanicilar(KullaniciID)
);
GO

CREATE TABLE Odemeler
(
    OdemeID INT PRIMARY KEY,
    GonderiID INT NOT NULL,
    Yontem VARCHAR(30) NOT NULL,
    Tutar DECIMAL(10,2) NOT NULL,
    ParaBirimi VARCHAR(10) NOT NULL DEFAULT 'TRY',
    OdemeDurumu VARCHAR(20) NOT NULL DEFAULT 'Baþarýlý',
    OdemeTarihi DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (GonderiID) REFERENCES Gonderiler(GonderiID),
    CONSTRAINT CK_OdemeYontem CHECK (Yontem IN ('Nakit', 'Kart', 'Online'))
);
GO

CREATE TABLE Teslimatlar
(
    TeslimatID INT PRIMARY KEY,
    GonderiID INT NOT NULL UNIQUE,
    TeslimatTarihi DATETIME NULL,
    TeslimAlanAdSoyad VARCHAR(100) NULL,
    TeslimAlanKimlikNo VARCHAR(20) NULL,
    Aciklama VARCHAR(200) NULL,
    BasariliMi BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (GonderiID) REFERENCES Gonderiler(GonderiID)
);
GO

CREATE TABLE Iadeler
(
    IadeID INT PRIMARY KEY,
    GonderiID INT NOT NULL UNIQUE,
    IadeNedeni VARCHAR(200) NOT NULL,
    IadeTarihi DATETIME NOT NULL DEFAULT GETDATE(),
    IadeDurumu VARCHAR(30) NOT NULL DEFAULT 'Ýþleniyor',
    FOREIGN KEY (GonderiID) REFERENCES Gonderiler(GonderiID)
);
GO



INSERT INTO Roller VALUES
(1, 'Admin'),
(2, 'Musteri'),
(3, 'Kurye');
GO

INSERT INTO Kullanicilar VALUES
(1, 'admin', 'admin123_hash', 1, 1, NULL),
(2, 'ahmet', 'ahmet123_hash', 2, 1, NULL),
(3, 'ayse', 'ayse123_hash', 2, 1, NULL),
(4, 'kerem', 'kerem123_hash', 3, 1, NULL),
(5, 'merve', 'merve123_hash', 3, 1, NULL);
GO

INSERT INTO Subeler VALUES
(1, 'Ýstanbul Avcýlar Þube', 'Ýstanbul', 'Avcýlar Merkez Mah. 1', '02120000001', 1),
(2, 'Ýstanbul Kadýköy Þube', 'Ýstanbul', 'Kadýköy Bahariye 22', '02120000002', 1),
(3, 'Ýzmir Konak Þube', 'Ýzmir', 'Konak Kemeraltý 5', '02320000003', 1),
(4, 'Ankara Çankaya Þube', 'Ankara', 'Çankaya Atakule 10', '03120000004', 1),
(5, 'Bursa Nilüfer Þube', 'Bursa', 'Nilüfer FSM 18', '02240000005', 1);
GO

INSERT INTO Araclar VALUES
(1, '34 ABC 123', 'Motosiklet', 35.00, 1),
(2, '34 XYZ 456', 'Panelvan', 750.00, 1),
(3, '35 KLM 789', 'Motosiklet', 40.00, 1),
(4, '06 ANK 001', 'Kamyonet', 1200.00, 1),
(5, '16 BRS 555', 'Panelvan', 800.00, 1);
GO

INSERT INTO Musteriler VALUES
(1, 'Ahmet Yýlmaz', '05001112233', 'ahmet.yilmaz@example.com', 'Marmara Cd. No:12', 'Ýstanbul', 'Türkiye', GETDATE(), 2),
(2, 'Ayþe Demir', '05002223344', 'ayse.demir@example.com', 'Atatürk Mh. 45', 'Ýzmir', 'Türkiye', GETDATE(), 3),
(3, 'Mehmet Kaya', '05003334455', 'mehmet.kaya@example.com', 'Çiðdem Sk. 8', 'Ankara', 'Türkiye', GETDATE(), NULL),
(4, 'Fatma Þahin', '05004445566', 'fatma.sahin@example.com', 'Gül Sk. 20', 'Bursa', 'Türkiye', GETDATE(), NULL),
(5, 'Ali Çelik', '05005556677', 'ali.celik@example.com', 'Papatya Cd. 3', 'Antalya', 'Türkiye', GETDATE(), NULL);
GO

INSERT INTO Kuryeler VALUES
(1, 'Kerem Taþ', '05001231234', 1, 1, 'Müsait', '2024-01-10', 4),
(2, 'Merve Ýnce', '05002342345', 1, 2, 'Daðýtýmda', '2024-02-15', 5),
(3, 'Can Er', '05003453456', 3, 3, 'Müsait', '2024-03-12', NULL),
(4, 'Selin Uz', '05004564567', 4, 4, 'Müsait', '2024-05-08', NULL),
(5, 'Emre Tok', '05005675678', 5, 5, 'Ýzinde', '2024-06-20', NULL);
GO

INSERT INTO Alicilar VALUES
(1, 'Zehra Kaya', '05009998877', 'zehra.kaya@example.com', 'Baðdat Cad. 88', 'Ýstanbul', 'Türkiye'),
(2, 'Murat Arslan', '05008887766', 'murat.arslan@example.com', 'Konak Meydaný 12', 'Ýzmir', 'Türkiye'),
(3, 'Selma Yurt', '05007776655', 'selma.yurt@example.com', 'Kýzýlay Sok. 40', 'Ankara', 'Türkiye'),
(4, 'Deniz Ak', '05006665544', 'deniz.ak@example.com', 'FSM Bulvarý 22', 'Bursa', 'Türkiye'),
(5, 'Canan Öz', '05005554433', 'canan.oz@example.com', 'Lara Cd. 5', 'Antalya', 'Türkiye');
GO

INSERT INTO GonderiDurumlari VALUES
('Oluþturuldu', 'Gönderi kaydý oluþturuldu'),
('Þubeye Ulaþtý', 'Gönderi ilgili þubeye ulaþtý'),
('Yolda', 'Gönderi transfer sürecinde'),
('Teslimatta', 'Gönderi daðýtýma çýktý'),
('Teslim Edildi', 'Gönderi alýcýya teslim edildi'),
('Ýade', 'Gönderi iade sürecine alýndý'),
('Ýptal', 'Gönderi iptal edildi'),
('Hata', 'Gönderi sürecinde hata oluþtu');
GO

INSERT INTO Gonderiler VALUES
(1, 'TRK0010001', 1, 1, 1, 2, 1, 'Oluþturuldu', 'Standart', 'Ödendi', 2.50, 0.60, '2025-12-01 09:00:00', '2025-12-03 18:00:00', NULL, NULL),
(2, 'TRK0010002', 2, 2, 3, 1, 3, 'Yolda', 'Ekspres', 'Ödendi', 3.20, 0.70, '2025-12-01 10:00:00', '2025-12-02 18:00:00', NULL, NULL),
(3, 'TRK0010003', 3, 3, 4, 5, 4, 'Teslimatta', 'Standart', 'Ödendi', 8.50, 1.60, '2025-12-02 08:30:00', '2025-12-03 20:00:00', NULL, NULL),
(4, 'TRK0010004', 4, 4, 5, 4, 5, 'Teslim Edildi', 'Ayný Gün', 'Ödendi', 1.80, 0.30, '2025-12-02 09:00:00', '2025-12-02 23:00:00', '2025-12-03 14:15:00', 'Deniz Ak'),
(5, 'TRK0010005', 5, 5, 2, 1, 2, 'Yolda', 'Standart', 'Bekliyor', 5.40, 1.10, '2025-12-03 11:00:00', '2025-12-05 18:00:00', NULL, NULL);
GO

INSERT INTO GonderiKalemleri VALUES
(1, 1, 'URUN-001', 'Akýllý Telefon', 0.50, '15x8x5', 1.00, 1, 1),
(2, 1, 'URUN-002', 'Telefon Kýlýfý', 0.10, '10x6x2', 0.50, 1, 0),
(3, 2, 'URUN-003', 'Kitap - Veri Bilimi', 0.70, '23x15x3', 1.20, 1, 0),
(4, 3, 'URUN-004', 'Laptop 14 inç', 1.60, '35x25x3', 8.50, 1, 1),
(5, 4, 'URUN-005', 'Kulaklýk', 0.30, '20x15x10', 1.80, 1, 0),
(6, 5, 'URUN-006', 'Ayakkabý', 1.10, '30x20x12', 5.40, 1, 0);
GO

INSERT INTO TakipOlaylari VALUES
(1, 1, '2025-12-01 09:05:00', 'Ýstanbul Avcýlar Þube', 'Oluþturuldu', 'Gönderi oluþturuldu', 1),
(2, 2, '2025-12-01 10:10:00', 'Ýzmir Konak Þube', 'Oluþturuldu', 'Gönderi kaydý alýndý', 1),
(3, 2, '2025-12-01 16:50:00', 'Ýstanbul Avcýlar Þube', 'Yolda', 'Araç transfer sürecinde', 1),
(4, 3, '2025-12-02 08:40:00', 'Ankara Çankaya Þube', 'Teslimatta', 'Teslimata çýktý', 1),
(5, 4, '2025-12-03 14:15:00', 'Bursa Nilüfer Þube', 'Teslim Edildi', 'Alýcýya teslim edildi', 1),
(6, 5, '2025-12-03 11:05:00', 'Ýstanbul Kadýköy Þube', 'Yolda', 'Transfer baþladý', 1);
GO

INSERT INTO Odemeler VALUES
(1, 1, 'Online', 49.90, 'TRY', 'Baþarýlý', '2025-12-01 09:06:00'),
(2, 2, 'Kart',   29.90, 'TRY', 'Baþarýlý', '2025-12-01 10:12:00'),
(3, 3, 'Nakit',  39.90, 'TRY', 'Baþarýlý', '2025-12-02 08:45:00'),
(4, 4, 'Online', 19.90, 'TRY', 'Baþarýlý', '2025-12-02 09:12:00'),
(5, 5, 'Kart',   34.90, 'TRY', 'Bekliyor', '2025-12-03 11:05:00');
GO

INSERT INTO Teslimatlar VALUES
(1, 4, '2025-12-03 14:15:00', 'Deniz Ak', '12345678901', 'Sorunsuz teslim edildi', 1);
GO



CREATE VIEW vw_GonderiDetaylari
AS
SELECT
    g.GonderiID,
    g.TakipNo,
    m.AdSoyad AS Gonderen,
    a.AdSoyad AS Alici,
    cs.SubeAdi AS CikisSube,
    vs.SubeAdi AS VarisSube,
    k.AdSoyad AS Kurye,
    g.Durum,
    g.GonderiTipi,
    g.OdemeDurumu,
    g.OlusturmaTarihi,
    g.TahminiTeslimTarihi,
    g.TeslimTarihi
FROM Gonderiler g
JOIN Musteriler m ON g.MusteriID = m.MusteriID
JOIN Alicilar a ON g.AliciID = a.AliciID
JOIN Subeler cs ON g.CikisSubeID = cs.SubeID
JOIN Subeler vs ON g.VarisSubeID = vs.SubeID
LEFT JOIN Kuryeler k ON g.KuryeID = k.KuryeID;
GO

CREATE VIEW vw_AktifKuryeler
AS
SELECT
    k.KuryeID,
    k.AdSoyad,
    k.Telefon,
    s.SubeAdi,
    a.AracPlaka,
    k.Durum
FROM Kuryeler k
JOIN Subeler s ON k.SubeID = s.SubeID
LEFT JOIN Araclar a ON k.AracID = a.AracID
WHERE k.Durum <> 'Pasif';
GO



CREATE PROC sp_YeniMusteriEkle
    @MusteriID INT,
    @AdSoyad VARCHAR(100),
    @Telefon VARCHAR(20),
    @Eposta VARCHAR(100),
    @Adres VARCHAR(200),
    @Sehir VARCHAR(100),
    @Ulke VARCHAR(100)
AS
BEGIN
    INSERT INTO Musteriler(MusteriID, AdSoyad, Telefon, Eposta, Adres, Sehir, Ulke)
    VALUES (@MusteriID, @AdSoyad, @Telefon, @Eposta, @Adres, @Sehir, @Ulke);
END;
GO

CREATE PROC sp_YeniGonderiOlustur
    @GonderiID INT,
    @TakipNo VARCHAR(30),
    @MusteriID INT,
    @AliciID INT,
    @CikisSubeID INT,
    @VarisSubeID INT,
    @KuryeID INT = NULL,
    @GonderiTipi VARCHAR(30) = 'Standart'
AS
BEGIN
    INSERT INTO Gonderiler
    (
        GonderiID, TakipNo, MusteriID, AliciID, CikisSubeID, VarisSubeID, KuryeID,
        Durum, GonderiTipi, OdemeDurumu, OlusturmaTarihi
    )
    VALUES
    (
        @GonderiID, @TakipNo, @MusteriID, @AliciID, @CikisSubeID, @VarisSubeID, @KuryeID,
        'Oluþturuldu', @GonderiTipi, 'Bekliyor', GETDATE()
    );
END;
GO

CREATE PROC sp_GonderiDurumGuncelle
    @GonderiID INT,
    @YeniDurum VARCHAR(30),
    @Notlar VARCHAR(200) = NULL
AS
BEGIN
    UPDATE Gonderiler
    SET Durum = @YeniDurum,
        TeslimTarihi = CASE WHEN @YeniDurum = 'Teslim Edildi' THEN GETDATE() ELSE TeslimTarihi END
    WHERE GonderiID = @GonderiID;

    INSERT INTO TakipOlaylari(OlayID, GonderiID, OlayZamani, Konum, Durum, Notlar)
    VALUES
    (
        (SELECT ISNULL(MAX(OlayID), 0) + 1 FROM TakipOlaylari),
        @GonderiID,
        GETDATE(),
        'Sistem Güncellemesi',
        @YeniDurum,
        @Notlar
    );
END;
GO

CREATE PROC sp_GonderiTakipOlaylari
    @GonderiID INT
AS
BEGIN
    SELECT *
    FROM TakipOlaylari
    WHERE GonderiID = @GonderiID
    ORDER BY OlayZamani;
END;
GO

CREATE PROC sp_SubeKuryeleri
    @SubeID INT
AS
BEGIN
    SELECT k.KuryeID, k.AdSoyad, k.Telefon, k.Durum, a.AracPlaka
    FROM Kuryeler k
    LEFT JOIN Araclar a ON k.AracID = a.AracID
    WHERE k.SubeID = @SubeID;
END;
GO

CREATE PROC sp_TeslimEdilenGonderiler
AS
BEGIN
    SELECT *
    FROM vw_GonderiDetaylari
    WHERE Durum = 'Teslim Edildi';
END;
GO

CREATE PROC sp_KuryeAta
    @GonderiID INT,
    @KuryeID INT
AS
BEGIN
    UPDATE Gonderiler
    SET KuryeID = @KuryeID
    WHERE GonderiID = @GonderiID;

    INSERT INTO TakipOlaylari(OlayID, GonderiID, OlayZamani, Konum, Durum, Notlar)
    VALUES
    (
        (SELECT ISNULL(MAX(OlayID), 0) + 1 FROM TakipOlaylari),
        @GonderiID,
        GETDATE(),
        'Þube Operasyonu',
        'Oluþturuldu',
        'Gönderi için kurye atamasý yapýldý'
    );
END;
GO

CREATE PROC sp_AylikGelirRaporu
    @Yil INT,
    @Ay INT
AS
BEGIN
    SELECT
        @Yil AS Yil,
        @Ay AS Ay,
        COUNT(*) AS OdemeSayisi,
        SUM(Tutar) AS ToplamGelir
    FROM Odemeler
    WHERE YEAR(OdemeTarihi) = @Yil
      AND MONTH(OdemeTarihi) = @Ay
      AND OdemeDurumu = 'Baþarýlý';
END;
GO



CREATE TRIGGER trg_GonderiOlusturuldu
ON Gonderiler
AFTER INSERT
AS
BEGIN
    INSERT INTO TakipOlaylari(OlayID, GonderiID, OlayZamani, Konum, Durum, Notlar)
    SELECT
        (SELECT ISNULL(MAX(OlayID), 0) FROM TakipOlaylari) + ROW_NUMBER() OVER (ORDER BY i.GonderiID),
        i.GonderiID,
        GETDATE(),
        'Ýlk Kayýt',
        i.Durum,
        'Gönderi sisteme eklendi'
    FROM inserted i;
END;
GO

CREATE TRIGGER trg_TeslimEdildiTarihiGuncelle
ON Gonderiler
AFTER UPDATE
AS
BEGIN
    UPDATE g
    SET TeslimTarihi = GETDATE()
    FROM Gonderiler g
    INNER JOIN inserted i ON g.GonderiID = i.GonderiID
    INNER JOIN deleted d ON d.GonderiID = i.GonderiID
    WHERE i.Durum = 'Teslim Edildi'
      AND d.Durum <> 'Teslim Edildi'
      AND g.TeslimTarihi IS NULL;
END;
GO



-- Tüm gönderi detaylarýný listele
SELECT * FROM vw_GonderiDetaylari;
GO

-- Þubeye göre aktif kurye listesi
SELECT * FROM vw_AktifKuryeler;
GO

-- Aralýk bazlý ödeme raporu
EXEC sp_AylikGelirRaporu @Yil = 2025, @Ay = 12;
GO
