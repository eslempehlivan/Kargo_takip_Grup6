using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class GonderiDurumlari
{
    public string DurumKodu { get; set; } = null!;

    public string Aciklama { get; set; } = null!;

    public virtual ICollection<Gonderiler> Gonderilers { get; set; } = new List<Gonderiler>();

    public virtual ICollection<TakipOlaylari> TakipOlaylaris { get; set; } = new List<TakipOlaylari>();
}
