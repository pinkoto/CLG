using System.ComponentModel.DataAnnotations;

namespace CLG.Core.Models
{
    public class CredentialsModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
