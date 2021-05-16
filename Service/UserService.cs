using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.User;
using TalktifAPI.Middleware;
using TalktifAPI.Models;
using TalktifAPI.Repository;
using BC = BCrypt.Net.BCrypt;

namespace TalktifAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userService;
        private readonly IUserRefreshTokenRepository _tokenService;
        private readonly IJwtService _jwtService;
        private readonly JwtConfig _JwtConfig;
        private readonly IReportRepository _reportRepository;

        public UserService(IUserRepository userService,IUserRefreshTokenRepository tokenService,
                    IJwtService jwtService, IOptions<JwtConfig> JwtConfig,IReportRepository reportRepository)
        {
            _userService = userService;
            _tokenService = tokenService;
            _jwtService = jwtService;
            _JwtConfig = JwtConfig.Value;
            _reportRepository = reportRepository;
        }
        public ReadUserDto getInfoById(int id)
        {
            User user = _userService.GetById(id);
            if(user==null) throw new Exception("user doesn't exist!");
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                                     IsAdmin = user.IsAdmin, IsActive = user.IsActive, Hobbies = user.Hobbies};
        }

        public bool inActiveUser(int id)
        {
            User u = _userService.GetById(id);
            if(u==null) return false;
            u.IsActive = false;
            return true;
        }

        public bool isUserExists(int id)
        {
            User u = _userService.GetById(id);
            if(u==null) return false;
            return true;
        }

        public RefreshTokenRespond RefreshToken(ReFreshToken token)
        {
            try{
            if(!_jwtService.ValidRefreshToken(new UserRefreshToken{
                Id = token.Id, 
                RefreshToken =  token.RefreshToken
            })) throw new SecurityTokenExpiredException();  
            var jwtToken = _jwtService.GenerateSecurityToken(_jwtService.GetRole(token.RefreshToken));
            return new RefreshTokenRespond{
                Token = jwtToken
            };
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool Report(ReportRequest request)
        {
            _reportRepository.Insert(new Report{
                Note = request.Note,
                Reporter = request.Reporter,
                Suspect = request.Suspect,
                Reason = request.Reason,
                Status = false,
            });
            return true;
        }

        public LoginRespond resetPass(string email, string newpass)
        {
            User user = _userService.GetUserByEmail(email);
            if(user == null){
                throw new Exception("Wrong email");
            }
            user.Password = BC.HashPassword(newpass);
            _userService.Update(user);
            var jwtToken = _jwtService.GenerateSecurityToken((bool)user.IsAdmin);
            return new LoginRespond(getInfoById(user.Id), jwtToken, null,0);
        }

        public LoginRespond signIn(LoginRequest user)
        { 
            User read = _userService.GetUserByEmail(user.Email);
            if(read==null) throw new Exception();
            if (true == BC.Verify(user.Password, read.Password) && read.IsActive == true && read.ConfirmedEmail==true){
                string token = _jwtService.GenerateSecurityToken((bool)read.IsAdmin);
                string refreshtoken = _jwtService.GenerateRefreshToken((bool)read.IsAdmin);
                _tokenService.Insert(new UserRefreshToken{
                    User = read.Id,RefreshToken = refreshtoken,
                    CreateAt = DateTime.Now,Device = user.Device});
                UserRefreshToken refreshToken = _tokenService.GetTokenByToken(read.Id);
                return new LoginRespond(new ReadUserDto { Email = read.Email, Name = read.Name,
                                                        Id = read.Id , Gender= read.Gender, IsAdmin = read.IsAdmin, 
                                                        Hobbies = read.Hobbies, IsActive = read.IsActive }, token,refreshtoken,refreshToken.Id);
            }
            throw new Exception("Wrong Password");
        }

        public SignUpRespond signUp(SignUpRequest user)
        {
            User read = _userService.GetUserByEmail(user.Email);
            if(read!=null) throw new Exception("User has already exist"+ read.Id);
            _userService.Insert(new User(user.Name,user.Email,BC.HashPassword(user.Password),user.Gender,user.Hobbies));
            read = _userService.GetUserByEmail(user.Email);
            string token = _jwtService.GenerateSecurityToken((bool)false);
            string refreshtoken = _jwtService.GenerateRefreshToken((bool)read.IsAdmin);
            _tokenService.Insert(new UserRefreshToken{
                User = read.Id,RefreshToken = refreshtoken,
                CreateAt = DateTime.Now,Device = user.Device});
            UserRefreshToken refreshToken = _tokenService.GetTokenByToken(read.Id);
            return new SignUpRespond(new ReadUserDto{ Id = read.Id, Email = user.Email,IsActive = read.IsActive,
                                                        Name = user.Name,IsAdmin = read.IsAdmin, 
                                                        Gender= user.Gender, Hobbies = user.Hobbies,}, token,refreshtoken,refreshToken.Id);
            throw new Exception("Wrong Password");
        }

        public ReadUserDto updateInfo(UpdateInfoRequest user)
        {
            if(user==null || !isUserExists(user.Id)) throw new Exception("Not found");
            User u = _userService.GetById(user.Id);
            u.Email = user.Email;
            u.Gender = user.Gender;
            u.Name = user.Name;
            u.Hobbies = user.Hobbies;
            _userService.Update(u);
            return new ReadUserDto { Email = user.Email,Name = user.Name,
                                    Id = u.Id ,Gender= user.Gender, IsAdmin = u.IsAdmin,  Hobbies = user.Hobbies, };
        }
        public bool ActiveEmail(string token, int id)
        {
            if(CheckToken(token,id)){
                UserRefreshToken t = _tokenService.GetById(id);
                User u = _userService.GetById(t.User);
                u.ConfirmedEmail = true;
                _userService.Update(u);
                return true;
            }
            return false;
        }

        public bool CheckToken(string token, int id)
        {
            UserRefreshToken t = _tokenService.GetById(id);
            if( t!= null && token.Equals(t.RefreshToken)) return true;
            return false;
        }

        public ReadUserDto getInfoByEmail(string email)
        {
            User user = _userService.GetUserByEmail(email);
            if(user==null) throw new Exception("user doesn't exist!");
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                                     IsAdmin = user.IsAdmin, IsActive = user.IsActive, Hobbies = user.Hobbies};
        }
    }   
}