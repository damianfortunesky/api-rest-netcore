using api_rest_netcore.Repository.IRepository;
using api_rest_netcore.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_rest_netcore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usRepo;

        // Constructor e Inyección de Dependencias  -  El controlador recibe la interfaz IUserRepository mediante el constructor.
        public UsersController(IUserRepository usRepo)
        {
            _usRepo = usRepo;
        }

        // GET: api/users  
        [HttpGet]
        public IActionResult GetUsers() //  IActionResult permite devolver respuestas HTTP flexibles (Ok, NotFound, etc.)
        {
            var users = _usRepo.GetUsers(); // Usa el método GetUsers del repositorio, que devuelve una colección de UserDto.
            return Ok(users); 
        }

        // GET: api/users/{id}
        [HttpGet("{id:int}", Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            var user = _usRepo.GetUser(id);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            return Ok(user); // Retorna un UserDto
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Valida el modelo
            }

            if (!_usRepo.IsUniqueUser(userRegisterDto.Username))
            {
                return Conflict(new { message = "El nombre de usuario ya está en uso." });
            }

            var user = await _usRepo.Register(userRegisterDto);

            return CreatedAtRoute("GetUser", new { id = user.Id }, user); // Retorna el UserDto recién creado  ej: http://localhost:5000/api/users/5
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Valida el modelo
            }

            var response = await _usRepo.Login(userLoginDto);

            if (string.IsNullOrEmpty(response.Token))
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });
            }

            return Ok(response); // Retorna el UserLoginResponseDto con el token
        }
    }
}
