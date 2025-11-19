using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsysWork.Models
{
    [Table("Shagirdler")]
    public class Shagird
    {
        [Key]
        public int Nomresi { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        [StringLength(30)]
        public string Adi { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(30)")]
        [StringLength(30)]
        public string Soyadi { get; set; } = string.Empty;

        [Required]
        public int Sinifi { get; set; }

        // Navigation property
        public virtual ICollection<Imtahan> Imtahanlar { get; set; } = new List<Imtahan>();
    }
}

