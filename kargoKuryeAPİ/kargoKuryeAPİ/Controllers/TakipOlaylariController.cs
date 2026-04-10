using kargoKuryeAPİ.Dtos;
using kargoKuryeAPİ.Dtos;
using kargoKuryeAPİ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kargoKuryeAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakipOlaylariController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TakipOlaylariController(AppDbContext context)
        {
            _context = context;
        }

        // Bir gönderinin takip geçmişini getir
        [HttpGet("{gonderiId}")]
        public async Task<IActionResult> GetByGonderiId(int gonderiId)
        {
            var takipler = await _context.TakipOlaylaris
                .Where(x => x.GonderiId == gonderiId)
                .OrderBy(x => x.OlayZamani)
                .ToListAsync();

            return Ok(takipler);
        }

        // Yeni takip olayı ekle
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
                gonderiId = yeniTakip.GonderiId,
                durum = yeniTakip.Durum,
                olayZamani = yeniTakip.OlayZamani,
                notlar = yeniTakip.Notlar
            });
        }
    }
}