using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class VwGonderiDetaylari
{
    public int GonderiId { get; set; }

    public string TakipNo { get; set; } = null!;

    public string Gonderen { get; set; } = null!;

    public string Alici { get; set; } = null!;

    public string CikisSube { get; set; } = null!;

    public string VarisSube { get; set; } = null!;

    public string? Kurye { get; set; }

    public string Durum { get; set; } = null!;

    public string GonderiTipi { get; set; } = null!;

    public string OdemeDurumu { get; set; } = null!;

    public DateTime OlusturmaTarihi { get; set; }

    public DateTime? TahminiTeslimTarihi { get; set; }

    public DateTime? TeslimTarihi { get; set; }
}
