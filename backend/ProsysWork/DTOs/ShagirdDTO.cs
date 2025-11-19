using System.ComponentModel.DataAnnotations;

namespace ProsysWork.DTOs
{
    public class ShagirdDTO
    {
        [Required]
        [Range(1, 99999)]
        public int Nomresi { get; set; }

        [Required]
        [StringLength(30)]
        public string Adi { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Soyadi { get; set; } = string.Empty;

        [Required]
        [Range(1, 12)]
        public int Sinifi { get; set; }
    }
}

