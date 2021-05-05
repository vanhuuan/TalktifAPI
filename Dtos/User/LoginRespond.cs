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
        public string Hobbies { get; set; }
        [Required]
        public string Token {get; set;}
        [Required]
        [JsonIgnore]
        public string RefreshToken {get; set;}
        public LoginRespond(ReadUserDto user , string token,string refreshtoken)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Gender = user.Gender;
            Hobbies = user.Hobbies;
            Token = token;
            RefreshToken = refreshtoken;
        }
    }
}