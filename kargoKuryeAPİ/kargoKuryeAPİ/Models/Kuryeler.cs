using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Kuryeler
{
    public int KuryeId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public int SubeId { get; set; }

    public int? AracId { get; set; }

    public string Durum { get; set; } = null!;

    public DateOnly? IseBaslamaTarihi { get; set; }

    public int? KullaniciId { get; set; }

    public virtual Araclar? Arac { get; set; }

    public virtual ICollection<Gonderiler> Gonderilers { get; set; } = new List<Gonderiler>();

    public virtual Kullanicilar? Kullanici { get; set; }

    public virtual Subeler Sube { get; set; } = null!;
}
