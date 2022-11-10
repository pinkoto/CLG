using System.ComponentModel.DataAnnotations;

namespace CLG.Infrastructure.Data.Models
{
    public class Credentials
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(64)]
        [Required]
        public string Password { get; set; }

        [StringLength(20)]
        [Key]
        public string Key { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Seen { get; set; }
    }
}
