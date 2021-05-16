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
        public string Hobbies { get; set; }
    }
}