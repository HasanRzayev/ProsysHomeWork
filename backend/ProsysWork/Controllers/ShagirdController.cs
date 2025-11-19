using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProsysWork.Data;
using ProsysWork.DTOs;
using ProsysWork.Models;

namespace ProsysWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShagirdController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShagirdController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShagirdDTO>>> GetShagirdler()
        {
            var shagirdler = await _context.Shagirdler
                .Select(s => new ShagirdDTO
                {
                    Nomresi = s.Nomresi,
                    Adi = s.Adi,
                    Soyadi = s.Soyadi,
                    Sinifi = s.Sinifi
                })
                .ToListAsync();

            return Ok(shagirdler);
        }

        [HttpGet("{nomresi}")]
        public async Task<ActionResult<ShagirdDTO>> GetShagird(int nomresi)
        {
            var shagird = await _context.Shagirdler.FindAsync(nomresi);

            if (shagird == null)
            {
                return NotFound();
            }

            var shagirdDTO = new ShagirdDTO
            {
                Nomresi = shagird.Nomresi,
                Adi = shagird.Adi,
                Soyadi = shagird.Soyadi,
                Sinifi = shagird.Sinifi
            };

            return Ok(shagirdDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ShagirdDTO>> PostShagird(ShagirdDTO shagirdDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shagird = new Shagird
            {
                Nomresi = shagirdDTO.Nomresi,
                Adi = shagirdDTO.Adi,
                Soyadi = shagirdDTO.Soyadi,
                Sinifi = shagirdDTO.Sinifi
            };

            _context.Shagirdler.Add(shagird);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShagirdExists(shagird.Nomresi))
                {
                    return Conflict("Bu şagird nömrəsi artıq mövcuddur.");
                }
                throw;
            }

            return CreatedAtAction(nameof(GetShagird), new { nomresi = shagird.Nomresi }, shagirdDTO);
        }

        [HttpPut("{nomresi}")]
        public async Task<IActionResult> PutShagird(int nomresi, ShagirdDTO shagirdDTO)
        {
            if (nomresi != shagirdDTO.Nomresi)
            {
                return BadRequest("Şagird nömrəsi uyğun deyil.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shagird = await _context.Shagirdler.FindAsync(nomresi);
            if (shagird == null)
            {
                return NotFound();
            }

            shagird.Adi = shagirdDTO.Adi;
            shagird.Soyadi = shagirdDTO.Soyadi;
            shagird.Sinifi = shagirdDTO.Sinifi;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShagirdExists(nomresi))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{nomresi}")]
        public async Task<IActionResult> DeleteShagird(int nomresi)
        {
            var shagird = await _context.Shagirdler.FindAsync(nomresi);
            if (shagird == null)
            {
                return NotFound();
            }

            var hasExams = await _context.Imtahanlar.AnyAsync(i => i.ShagirdNomresi == nomresi);
            if (hasExams)
            {
                return BadRequest("Bu şagird üçün imtahanlar mövcuddur. Əvvəlcə imtahanları silin.");
            }

            _context.Shagirdler.Remove(shagird);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShagirdExists(int nomresi)
        {
            return _context.Shagirdler.Any(e => e.Nomresi == nomresi);
        }
    }
}

