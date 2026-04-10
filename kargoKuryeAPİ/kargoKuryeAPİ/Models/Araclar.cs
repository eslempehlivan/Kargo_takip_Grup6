using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Araclar
{
    public int AracId { get; set; }

    public string AracPlaka { get; set; } = null!;

    public string AracTipi { get; set; } = null!;

    public decimal? KapasiteKg { get; set; }

    public bool AktifMi { get; set; }

    public virtual ICollection<Kuryeler> Kuryelers { get; set; } = new List<Kuryeler>();
}
