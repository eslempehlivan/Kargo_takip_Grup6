using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Teslimatlar
{
    public int TeslimatId { get; set; }

    public int GonderiId { get; set; }

    public DateTime? TeslimatTarihi { get; set; }

    public string? TeslimAlanAdSoyad { get; set; }

    public string? TeslimAlanKimlikNo { get; set; }

    public string? Aciklama { get; set; }

    public bool BasariliMi { get; set; }

    public virtual Gonderiler Gonderi { get; set; } = null!;
}
