using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class SignUpRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public String Password { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        [StringLength(200)]
        public string ForgotPass { get; set; }
        [Required]
        [StringLength(1000)]
        public string Device { get; set; }
    }
}