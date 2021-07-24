using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class UpdateInfoRequest
    {
        [Required]
        public int Id { get; set; }    
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(100)]
        public String Hobbies { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        [StringLength(200)]
        public string OldPassword { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}