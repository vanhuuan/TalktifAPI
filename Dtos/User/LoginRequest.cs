using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class LoginRequest {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(1000)]
        public string Device { get; set; }
    }
}