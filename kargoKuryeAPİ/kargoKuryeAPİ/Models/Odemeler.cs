using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Odemeler
{
    public int OdemeId { get; set; }

    public int GonderiId { get; set; }

    public string Yontem { get; set; } = null!;

    public decimal Tutar { get; set; }

    public string ParaBirimi { get; set; } = null!;

    public string OdemeDurumu { get; set; } = null!;

    public DateTime OdemeTarihi { get; set; }

    public virtual Gonderiler Gonderi { get; set; } = null!;
}
