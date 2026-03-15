using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareBusiness.Models
{
    public partial class Session
    {
        [Key]
        [StringLength(50)]
        public string SessionID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ExpiresAt { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty("Sessions")]
        public virtual User User { get; set; }
    }
}
