using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class TakipOlaylari
{
    public int OlayId { get; set; }

    public int GonderiId { get; set; }

    public DateTime OlayZamani { get; set; }

    public string Konum { get; set; } = null!;

    public string Durum { get; set; } = null!;

    public string? Notlar { get; set; }

    public int? IslemYapanKullaniciId { get; set; }

    public virtual GonderiDurumlari DurumNavigation { get; set; } = null!;

    public virtual Gonderiler Gonderi { get; set; } = null!;

    public virtual Kullanicilar? IslemYapanKullanici { get; set; }
}
