using kargoKuryeAPİ.Dtos;
namespace kargoKuryeAPİ.Dtos
{
    public class GonderiCreateDto
    {
        public string TakipNo { get; set; }
        public int MusteriId { get; set; }
        public int AliciId { get; set; }
        public int CikisSubeId { get; set; }
        public int VarisSubeId { get; set; }
        public int? KuryeId { get; set; }
        public string Durum { get; set; }
        public string GonderiTipi { get; set; }
        public string OdemeDurumu { get; set; }
        public decimal? ToplamDesi { get; set; }
        public decimal? ToplamAgirlikKg { get; set; }
    }
}