using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Kullanicilar
{
    public int KullaniciId { get; set; }

    public string KullaniciAdi { get; set; } = null!;

    public string SifreHash { get; set; } = null!;

    public int RolId { get; set; }

    public bool AktifMi { get; set; }

    public DateTime? SonGirisTarihi { get; set; }

    public virtual ICollection<Kuryeler> Kuryelers { get; set; } = new List<Kuryeler>();

    public virtual ICollection<Musteriler> Musterilers { get; set; } = new List<Musteriler>();

    public virtual Roller Rol { get; set; } = null!;

    public virtual ICollection<TakipOlaylari> TakipOlaylaris { get; set; } = new List<TakipOlaylari>();
}
