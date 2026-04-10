using System;
using System.Collections.Generic;

namespace kargoKuryeAPİ.Models;

public partial class Iadeler
{
    public int IadeId { get; set; }

    public int GonderiId { get; set; }

    public string IadeNedeni { get; set; } = null!;

    public DateTime IadeTarihi { get; set; }

    public string IadeDurumu { get; set; } = null!;

    public virtual Gonderiler Gonderi { get; set; } = null!;
}
