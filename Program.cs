using api_rest_netcore.Data;
using api_rest_netcore.Repository.IRepository;
using api_rest_netcore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

/* Injeccion dependencias */

// SQL Conexion
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

// Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// SecretKey
var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey")
           ?? throw new InvalidOperationException("ApiSettings:SecretKey no está configurado.");

// CORS - Config
builder.Services.AddCors(p => p.AddPolicy("PolicyCORS", build => 
{
    build.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
}));

//Autenticacion
builder.Services.AddAuthentication
    (
        p =>
        {
            // Establece el esquema predeterminado de autenticación y desafíos como JWT Bearer
            p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }
    ).AddJwtBearer
    (
        p =>
        {       
            p.RequireHttpsMetadata = false;                                                 // Permite solicitudes HTTP sin HTTPS (útil en desarrollo). Cambiar a true en producción.           
            p.SaveToken = true;                                                             // Guarda el token validado en el contexto de autenticación para su posterior uso.            
            p.TokenValidationParameters = new TokenValidationParameters                     // Parámetros de validación del token JWT
            {
               
                ValidateIssuerSigningKey = true,                                            // Habilita la validación de la firma del token                
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),  // Define la clave simétrica utilizada para validar la firma del token                
                ValidateIssuer = false,                                                     // Desactiva la validación del emisor (Issuer). Cambiar a true si se quiere validar el emisor del token.               
                ValidateAudience = false                                                    // Desactiva la validación de la audiencia (Audience). Cambiar a true si los tokens están destinados a una audiencia específica.
            };
        }   
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Agregamos CORS configurado
app.UseCors("PolicyCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
