using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Musteriler
{
    public int MusteriId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string Eposta { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Sehir { get; set; } = null!;

    public string Ulke { get; set; } = null!;

    public DateTime KayitTarihi { get; set; }

    public int? KullaniciId { get; set; }

    public virtual ICollection<Gonderiler> Gonderilers { get; set; } = new List<Gonderiler>();

    public virtual Kullanicilar? Kullanici { get; set; }
}
