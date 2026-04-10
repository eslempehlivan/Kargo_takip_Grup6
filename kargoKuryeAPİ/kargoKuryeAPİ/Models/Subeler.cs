using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Subeler
{
    public int SubeId { get; set; }

    public string SubeAdi { get; set; } = null!;

    public string Sehir { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string? Telefon { get; set; }

    public bool AcikMi { get; set; }

    public virtual ICollection<Gonderiler> GonderilerCikisSubes { get; set; } = new List<Gonderiler>();

    public virtual ICollection<Gonderiler> GonderilerVarisSubes { get; set; } = new List<Gonderiler>();

    public virtual ICollection<Kuryeler> Kuryelers { get; set; } = new List<Kuryeler>();
}
