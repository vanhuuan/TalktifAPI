using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TalktifAPI.Models;

namespace TalktifAPI.Dtos
{
    public class LoginRespond
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public bool? Gender { get; set; }
        public bool? IsAdmin {get; set;}
        public bool? IsActive {get; set;}
        public string Hobbies { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Token {get; set;}
        public LoginRespond(ReadUserDto user , string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            IsAdmin = user.IsAdmin;
            Gender = user.Gender;
            Hobbies = user.Hobbies;
            CityId = user.CityId;
            IsActive = user.IsActive;
            Token = token;
        }
    }
}