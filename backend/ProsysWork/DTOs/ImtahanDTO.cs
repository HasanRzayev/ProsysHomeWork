using System.ComponentModel.DataAnnotations;

namespace ProsysWork.DTOs
{
    public class ImtahanDTO
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string DersKodu { get; set; } = string.Empty;

        [Required]
        [Range(1, 99999)]
        public int ShagirdNomresi { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ImtahanTarixi { get; set; }

        [Required]
        [Range(1, 5)]
        public int Qiymeti { get; set; }
    }
}

