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
using XAct.Library.Settings;

namespace api_rest_netcore.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly ApplicationDbContext _db;

        public ContactRepository(ApplicationDbContext db)
        {
            _db = db;         
        }

        public async Task<ContactResponseDto> Register(ContactDto contactDtoValues)
        {
            // Crear una nueva instancia de Contact usando los valores del parámetro contactDtoValues
            var contact = new Contact
            {
                ContactName = contactDtoValues.ContactName,
                Description = contactDtoValues.Description,
                CreatedAt = DateTime.Now // Fecha actual
            };

            // Agregamos el objeto al DbContext
            _db.Add(contact);
            await _db.SaveChangesAsync();

            // Devolvemos una respuesta DTO
            return new ContactResponseDto
            {
                ContactId = contact.ContactId
            };
        }
    }
}


