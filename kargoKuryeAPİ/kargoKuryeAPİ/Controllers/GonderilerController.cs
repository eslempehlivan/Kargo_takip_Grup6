using kargoKuryeAPİ.Models;
using kargoKuryeAPİ.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace kargoKuryeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GonderilerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GonderilerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gonderiler = await _context.Gonderilers.ToListAsync();
            return Ok(gonderiler);
        }

        [HttpGet("{takipNo}")]
        public async Task<IActionResult> GetByTakipNo(string takipNo)
        {
            var gonderi = await _context.Gonderilers
                .FirstOrDefaultAsync(x => x.TakipNo == takipNo);

            if (gonderi == null)
                return NotFound("Gönderi bulunamadı.");

            return Ok(gonderi);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TakipOlayiCreateDto dto)
        {
            var gonderi = await _context.Gonderilers.FindAsync(dto.GonderiId);

            if (gonderi == null)
                return NotFound("Gönderi bulunamadı.");

            var yeniOlayId = 1;

            if (_context.TakipOlaylaris.Any())
            {
                yeniOlayId = _context.TakipOlaylaris.Max(x => x.OlayId) + 1;
            }

            var yeniTakip = new TakipOlaylari
            {
                OlayId = yeniOlayId,
                GonderiId = dto.GonderiId,
                Durum = dto.DurumKodu,
                Notlar = dto.Aciklama,
                OlayZamani = DateTime.Now,
                Konum = "Sistem"
            };

            _context.TakipOlaylaris.Add(yeniTakip);

            gonderi.Durum = dto.DurumKodu;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mesaj = "Takip olayı eklendi.",
                olayId = yeniTakip.OlayId,
                gonderiId = yeniTakip.GonderiId,
                durum = yeniTakip.Durum,
                olayZamani = yeniTakip.OlayZamani,
                notlar = yeniTakip.Notlar
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GonderiUpdateDto dto)
        {
            var gonderi = await _context.Gonderilers.FindAsync(id);

            if (gonderi == null)
                return NotFound("Gönderi bulunamadı.");

            gonderi.TakipNo = dto.TakipNo;
            gonderi.MusteriId = dto.MusteriId;
            gonderi.AliciId = dto.AliciId;
            gonderi.CikisSubeId = dto.CikisSubeId;
            gonderi.VarisSubeId = dto.VarisSubeId;
            gonderi.KuryeId = dto.KuryeId;
            gonderi.Durum = dto.Durum;
            gonderi.GonderiTipi = dto.GonderiTipi;
            gonderi.OdemeDurumu = dto.OdemeDurumu;
            gonderi.ToplamDesi = dto.ToplamDesi;
            gonderi.ToplamAgirlikKg = dto.ToplamAgirlikKg;

            await _context.SaveChangesAsync();

            return Ok(gonderi);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gonderi = await _context.Gonderilers.FindAsync(id);

            if (gonderi == null)
                return NotFound("Gönderi bulunamadı.");

            var takipKayitlari = await _context.TakipOlaylaris
                .Where(x => x.GonderiId == id)
                .ToListAsync();

            if (takipKayitlari.Any())
            {
                _context.TakipOlaylaris.RemoveRange(takipKayitlari);
            }

            _context.Gonderilers.Remove(gonderi);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mesaj = "Gönderi ve bağlı takip kayıtları başarıyla silindi.",
                silinenGonderiId = id
            });
        }
    }
}