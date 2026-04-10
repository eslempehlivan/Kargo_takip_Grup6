using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Alicilar
{
    public int AliciId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string? Eposta { get; set; }

    public string Adres { get; set; } = null!;

    public string Sehir { get; set; } = null!;

    public string Ulke { get; set; } = null!;

    public virtual ICollection<Gonderiler> Gonderilers { get; set; } = new List<Gonderiler>();
}
