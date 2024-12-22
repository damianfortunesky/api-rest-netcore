using api_rest_netcore.Models.Dtos;

namespace api_rest_netcore.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<UserDto> GetUsers(); // Devuelve una colección de UserDto, no del modelo directamente
        UserDto? GetUser(int userId); // Devuelve un UserDto o null si no se encuentra
        bool IsUniqueUser(string username);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
        Task<UserDto> Register(UserRegisterDto userRegisterDto); // Devuelve un UserDto en lugar de User
    }
}

