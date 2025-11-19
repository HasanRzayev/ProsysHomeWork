using System.ComponentModel.DataAnnotations;

namespace ProsysWork.DTOs
{
    public class DersDTO
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string DersKodu { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string DersAdi { get; set; } = string.Empty;

        [Required]
        [Range(1, 12)]
        public int Sinifi { get; set; }

        [Required]
        [StringLength(20)]
        public string MuellimAdi { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string MuellimSoyadi { get; set; } = string.Empty;
    }
}

