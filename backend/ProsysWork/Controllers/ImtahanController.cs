using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProsysWork.Data;
using ProsysWork.DTOs;
using ProsysWork.Models;

namespace ProsysWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImtahanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImtahanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImtahanDTO>>> GetImtahanlar()
        {
            var imtahanlar = await _context.Imtahanlar
                .Include(i => i.Ders)
                .Include(i => i.Shagird)
                .Select(i => new ImtahanDTO
                {
                    DersKodu = i.DersKodu,
                    ShagirdNomresi = i.ShagirdNomresi,
                    ImtahanTarixi = i.ImtahanTarixi,
                    Qiymeti = i.Qiymeti
                })
                .ToListAsync();

            return Ok(imtahanlar);
        }

        [HttpGet("{dersKodu}/{shagirdNomresi}/{tarix}")]
        public async Task<ActionResult<ImtahanDTO>> GetImtahan(string dersKodu, int shagirdNomresi, DateTime tarix)
        {
            var imtahan = await _context.Imtahanlar
                .FirstOrDefaultAsync(i => i.DersKodu == dersKodu 
                    && i.ShagirdNomresi == shagirdNomresi 
                    && i.ImtahanTarixi.Date == tarix.Date);

            if (imtahan == null)
            {
                return NotFound();
            }

            var imtahanDTO = new ImtahanDTO
            {
                DersKodu = imtahan.DersKodu,
                ShagirdNomresi = imtahan.ShagirdNomresi,
                ImtahanTarixi = imtahan.ImtahanTarixi,
                Qiymeti = imtahan.Qiymeti
            };

            return Ok(imtahanDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ImtahanDTO>> PostImtahan(ImtahanDTO imtahanDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dersExists = await _context.Dersler.AnyAsync(d => d.DersKodu == imtahanDTO.DersKodu);
            if (!dersExists)
            {
                return BadRequest("Dərs tapılmadı.");
            }

            var shagirdExists = await _context.Shagirdler.AnyAsync(s => s.Nomresi == imtahanDTO.ShagirdNomresi);
            if (!shagirdExists)
            {
                return BadRequest("Şagird tapılmadı.");
            }

            var imtahan = new Imtahan
            {
                DersKodu = imtahanDTO.DersKodu,
                ShagirdNomresi = imtahanDTO.ShagirdNomresi,
                ImtahanTarixi = imtahanDTO.ImtahanTarixi.Date,
                Qiymeti = imtahanDTO.Qiymeti
            };

            _context.Imtahanlar.Add(imtahan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImtahanExists(imtahan.DersKodu, imtahan.ShagirdNomresi, imtahan.ImtahanTarixi))
                {
                    return Conflict("Bu imtahan artıq mövcuddur.");
                }
                throw;
            }

            return CreatedAtAction(
                nameof(GetImtahan), 
                new { 
                    dersKodu = imtahan.DersKodu, 
                    shagirdNomresi = imtahan.ShagirdNomresi, 
                    tarix = imtahan.ImtahanTarixi 
                }, 
                imtahanDTO);
        }

        [HttpPut("{dersKodu}/{shagirdNomresi}/{tarix}")]
        public async Task<IActionResult> PutImtahan(string dersKodu, int shagirdNomresi, DateTime tarix, ImtahanDTO imtahanDTO)
        {
            if (dersKodu != imtahanDTO.DersKodu || shagirdNomresi != imtahanDTO.ShagirdNomresi || tarix.Date != imtahanDTO.ImtahanTarixi.Date)
            {
                return BadRequest("İmtahan parametrləri uyğun deyil.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imtahan = await _context.Imtahanlar
                .FirstOrDefaultAsync(i => i.DersKodu == dersKodu 
                    && i.ShagirdNomresi == shagirdNomresi 
                    && i.ImtahanTarixi.Date == tarix.Date);

            if (imtahan == null)
            {
                return NotFound();
            }

            imtahan.Qiymeti = imtahanDTO.Qiymeti;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImtahanExists(dersKodu, shagirdNomresi, tarix))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{dersKodu}/{shagirdNomresi}/{tarix}")]
        public async Task<IActionResult> DeleteImtahan(string dersKodu, int shagirdNomresi, DateTime tarix)
        {
            var imtahan = await _context.Imtahanlar
                .FirstOrDefaultAsync(i => i.DersKodu == dersKodu 
                    && i.ShagirdNomresi == shagirdNomresi 
                    && i.ImtahanTarixi.Date == tarix.Date);

            if (imtahan == null)
            {
                return NotFound();
            }

            _context.Imtahanlar.Remove(imtahan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImtahanExists(string dersKodu, int shagirdNomresi, DateTime tarix)
        {
            return _context.Imtahanlar.Any(e => 
                e.DersKodu == dersKodu 
                && e.ShagirdNomresi == shagirdNomresi 
                && e.ImtahanTarixi.Date == tarix.Date);
        }
    }
}

