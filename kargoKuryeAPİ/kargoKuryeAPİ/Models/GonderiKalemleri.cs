using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class GonderiKalemleri
{
    public int KalemId { get; set; }

    public int GonderiId { get; set; }

    public string UrunKodu { get; set; } = null!;

    public string Aciklama { get; set; } = null!;

    public decimal AgirlikKg { get; set; }

    public string? BoyutCm { get; set; }

    public decimal? Desi { get; set; }

    public int Adet { get; set; }

    public bool KırılabilirMi { get; set; }

    public virtual Gonderiler Gonderi { get; set; } = null!;
}
