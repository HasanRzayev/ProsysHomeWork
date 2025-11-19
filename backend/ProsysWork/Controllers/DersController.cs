using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProsysWork.Data;
using ProsysWork.DTOs;
using ProsysWork.Models;

namespace ProsysWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DersDTO>>> GetDersler()
        {
            var dersler = await _context.Dersler
                .Select(d => new DersDTO
                {
                    DersKodu = d.DersKodu,
                    DersAdi = d.DersAdi,
                    Sinifi = d.Sinifi,
                    MuellimAdi = d.MuellimAdi,
                    MuellimSoyadi = d.MuellimSoyadi
                })
                .ToListAsync();

            return Ok(dersler);
        }

        [HttpGet("{dersKodu}")]
        public async Task<ActionResult<DersDTO>> GetDers(string dersKodu)
        {
            var ders = await _context.Dersler.FindAsync(dersKodu);

            if (ders == null)
            {
                return NotFound();
            }

            var dersDTO = new DersDTO
            {
                DersKodu = ders.DersKodu,
                DersAdi = ders.DersAdi,
                Sinifi = ders.Sinifi,
                MuellimAdi = ders.MuellimAdi,
                MuellimSoyadi = ders.MuellimSoyadi
            };

            return Ok(dersDTO);
        }

        [HttpPost]
        public async Task<ActionResult<DersDTO>> PostDers(DersDTO dersDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ders = new Ders
            {
                DersKodu = dersDTO.DersKodu,
                DersAdi = dersDTO.DersAdi,
                Sinifi = dersDTO.Sinifi,
                MuellimAdi = dersDTO.MuellimAdi,
                MuellimSoyadi = dersDTO.MuellimSoyadi
            };

            _context.Dersler.Add(ders);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DersExists(ders.DersKodu))
                {
                    return Conflict("Bu dərs kodu artıq mövcuddur.");
                }
                throw;
            }

            return CreatedAtAction(nameof(GetDers), new { dersKodu = ders.DersKodu }, dersDTO);
        }

        [HttpPut("{dersKodu}")]
        public async Task<IActionResult> PutDers(string dersKodu, DersDTO dersDTO)
        {
            if (dersKodu != dersDTO.DersKodu)
            {
                return BadRequest("Dərs kodu uyğun deyil.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ders = await _context.Dersler.FindAsync(dersKodu);
            if (ders == null)
            {
                return NotFound();
            }

            ders.DersAdi = dersDTO.DersAdi;
            ders.Sinifi = dersDTO.Sinifi;
            ders.MuellimAdi = dersDTO.MuellimAdi;
            ders.MuellimSoyadi = dersDTO.MuellimSoyadi;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DersExists(dersKodu))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{dersKodu}")]
        public async Task<IActionResult> DeleteDers(string dersKodu)
        {
            var ders = await _context.Dersler.FindAsync(dersKodu);
            if (ders == null)
            {
                return NotFound();
            }

            var hasExams = await _context.Imtahanlar.AnyAsync(i => i.DersKodu == dersKodu);
            if (hasExams)
            {
                return BadRequest("Bu dərs üçün imtahanlar mövcuddur. Əvvəlcə imtahanları silin.");
            }

            _context.Dersler.Remove(ders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DersExists(string dersKodu)
        {
            return _context.Dersler.Any(e => e.DersKodu == dersKodu);
        }
    }
}

