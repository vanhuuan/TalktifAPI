using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TalktifAPI.Dtos
{
    public class ReFreshToken
    {
        [JsonIgnore]
        public int Id { get; set; }       
        [Required]
        [StringLength(1000)]
        public string RefreshToken { get; set; }
        [Required]
        [StringLength(100)]
        public string Device { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}