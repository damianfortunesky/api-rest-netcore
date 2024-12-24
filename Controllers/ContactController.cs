using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using api_rest_netcore.Models.Dtos;
using api_rest_netcore.Repository;
using api_rest_netcore.Repository.IRepository;

namespace api_rest_netcore.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // POST: api/Contacts/Register
        [HttpPost("register")]
        public async Task<ActionResult<ContactResponseDto>> Register([FromBody] ContactDto contactDto)
        {
            if (contactDto == null)
            {
                return BadRequest("Contact data is required.");
            }

            // Llamada al repositorio para registrar el contacto
            var response = await _contactRepository.Register(contactDto);

            if (response == null)
            {
                return StatusCode(500, "An error occurred while creating the contact.");
            }

            return Ok(response);
        }
    }
}
