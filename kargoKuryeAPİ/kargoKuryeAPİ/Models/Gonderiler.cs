using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Gonderiler
{
    public int GonderiId { get; set; }

    public string TakipNo { get; set; } = null!;

    public int MusteriId { get; set; }

    public int AliciId { get; set; }

    public int CikisSubeId { get; set; }

    public int VarisSubeId { get; set; }

    public int? KuryeId { get; set; }

    public string Durum { get; set; } = null!;

    public string GonderiTipi { get; set; } = null!;

    public string OdemeDurumu { get; set; } = null!;

    public decimal? ToplamDesi { get; set; }

    public decimal? ToplamAgirlikKg { get; set; }

    public DateTime OlusturmaTarihi { get; set; }

    public DateTime? TahminiTeslimTarihi { get; set; }

    public DateTime? TeslimTarihi { get; set; }

    public string? TeslimAlan { get; set; }

    public virtual Alicilar Alici { get; set; } = null!;

    public virtual Subeler CikisSube { get; set; } = null!;

    public virtual GonderiDurumlari DurumNavigation { get; set; } = null!;

    public virtual ICollection<GonderiKalemleri> GonderiKalemleris { get; set; } = new List<GonderiKalemleri>();

    public virtual Iadeler? Iadeler { get; set; }

    public virtual Kuryeler? Kurye { get; set; }

    public virtual Musteriler Musteri { get; set; } = null!;

    public virtual ICollection<Odemeler> Odemelers { get; set; } = new List<Odemeler>();

    public virtual ICollection<TakipOlaylari> TakipOlaylaris { get; set; } = new List<TakipOlaylari>();

    public virtual Teslimatlar? Teslimatlar { get; set; }

    public virtual Subeler VarisSube { get; set; } = null!;
}
