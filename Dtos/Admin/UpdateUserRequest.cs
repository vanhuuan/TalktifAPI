using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos.Admin
{
    public class UpdateUserRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? Gender { get; set; }
        public bool? IsActive { get; set; }
        public int CityId { get; set; }
    }
}