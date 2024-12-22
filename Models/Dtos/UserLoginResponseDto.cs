namespace api_rest_netcore.Models.Dtos
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; } = string.Empty; // Valor por defecto
        public UserDto? UserLogin { get; set; } // Permitir valores null
    }
}
