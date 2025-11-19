using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsysWork.Models
{
    [Table("Imtahanlar")]
    public class Imtahan
    {
        [Column(TypeName = "char(3)")]
        [StringLength(3)]
        public string DersKodu { get; set; } = string.Empty;

        public int ShagirdNomresi { get; set; }

        [Column(TypeName = "date")]
        public DateTime ImtahanTarixi { get; set; }

        [Required]
        [Range(1, 5)]
        public int Qiymeti { get; set; }

        // Navigation properties
        [ForeignKey("DersKodu")]
        public virtual Ders Ders { get; set; } = null!;

        [ForeignKey("ShagirdNomresi")]
        public virtual Shagird Shagird { get; set; } = null!;
    }
}

