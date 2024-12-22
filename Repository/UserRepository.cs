using api_rest_netcore.Data;
using api_rest_netcore.Models;
using api_rest_netcore.Models.Dtos;
using api_rest_netcore.Repository.IRepository;
using api_rest_netcore.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using api_rest_netcore.Modelos;
using Microsoft.EntityFrameworkCore;

namespace api_rest_netcore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly string _secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _secretKey = config.GetValue<string>("ApiSettings:SecretKey") ?? throw new ArgumentNullException("ApiSettings:SecretKey", "La clave secreta no está configurada.");
        }

        public UserDto? GetUser(int userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }

        public ICollection<UserDto> GetUsers()
        {
            return _db.Users.OrderBy(u => u.Id).Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            }).ToList();
        }

        public bool IsUniqueUser(string username)
        {
            return !_db.Users.Any(u => u.Username == username);
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var hashedPassword = EncryptToMd5.Encrypt(userLoginDto.Password);

            var user = await _db.Users.FirstOrDefaultAsync(
                u => u.Username.ToLower() == userLoginDto.Username.ToLower() && u.PasswordHash == hashedPassword
            );

            if (user == null)
            {
                return new UserLoginResponseDto
                {
                    Token = "",
                    UserLogin = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserLoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                UserLogin = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive
                }
            };
        }

        public async Task<UserDto> Register(UserRegisterDto userRegisterDto)
        {

            // Encriptar la contraseña
            var hashedPassword = EncryptToMd5.Encrypt(userRegisterDto.Password);

            // Crear el modelo User para interactuar con la base de datos
            var user = new User
            {
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                PasswordHash = hashedPassword,
                Role = 1, // Rol por defecto
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            // Agregar el usuario al DbContext
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // Mapear el modelo User al DTO UserDto para la respuesta
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }
    }
}
