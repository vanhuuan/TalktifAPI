using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class ReFreshToken
    {
        [Required]
        [StringLength(1000)]
        public string RefreshToken { get; set; }
        [Required]
        [StringLength(100)]
        public string Device { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}