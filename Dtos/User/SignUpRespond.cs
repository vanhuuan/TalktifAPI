using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TalktifAPI.Dtos
{
    public class SignUpRespond
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
        public string Token {get; set;}
        [Required]
        [JsonIgnore]                
        public string RefreshToken {get; set;}
        [Required]
        [JsonIgnore]                
        public int RefreshTokenId {get; set;}
        
        public SignUpRespond(ReadUserDto user , string token,string refreshtoken,int refreshtokenId)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Gender = user.Gender;
            Hobbies = user.Hobbies;
            IsActive = user.IsActive;
            IsAdmin = user.IsAdmin;
            Token = token;
            RefreshToken = refreshtoken;
            RefreshTokenId = refreshtokenId;
        }
    }
}