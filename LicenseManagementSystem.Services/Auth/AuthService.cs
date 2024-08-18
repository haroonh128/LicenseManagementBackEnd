using AutoMapper;
using LicenseManagementSystem.DA;
using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Login;
using LicenseManagementSystem.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LicenseManagementSystem.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public AuthService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse> RegisterUser(UserModel user)
        {
            ApiResponse response = new ApiResponse();
            var userData = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userData == null)
            {

                var newUser = _mapper.Map<User>(user);
                await _context.AddAsync(newUser);
                _context.SaveChanges();
                response.Status = 200;
                response.Message = "Successful User Creation";
            }
            return response;
        }

        public async Task<ApiResponse> Login(LoginRequest request)
        {
            ApiResponse response = new ApiResponse();
            var userData = _context.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
            if (userData != null)
            {
                var userRes = _mapper.Map<UserModel>(userData);
                response.Response = await GenerateToken(userRes);
                response.Status = 200;
                response.Message = "Login Successful";
            }
            return response;
        }


        private async Task<string> GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("gbskbgrieyuhgbaiegbrailerbgaqeiurbgaprigh387405zlkvb");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Username", user.UserName ?? ""),
                        new Claim("Email", user.Email),
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenGenerated = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenGenerated);
        }
    }
}
