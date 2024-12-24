using api_rest_netcore.Models.Dtos;

namespace api_rest_netcore.Repository.IRepository
{
    public interface IContactRepository
    {
        Task<ContactResponseDto> Register(ContactDto ContactValues); 
    }
}


