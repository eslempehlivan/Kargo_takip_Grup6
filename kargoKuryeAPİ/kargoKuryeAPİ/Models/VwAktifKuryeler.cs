using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class VwAktifKuryeler
{
    public int KuryeId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string SubeAdi { get; set; } = null!;

    public string? AracPlaka { get; set; }

    public string Durum { get; set; } = null!;
}
