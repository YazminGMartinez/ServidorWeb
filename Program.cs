using Microsoft.AspNetCore.Http.Json;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("/", () => "Hello World!");

app.MapPost("/usuarios/registrarme", UsuariosRequestHandler.Registrarme);
app.MapPost("/usuarios/acceso", UsuariosRequestHandler.Ingresar);
app.MapPost("/usuarios/recuperar", UsuariosRequestHandler.Recuperar);
app.MapPost("/categorias/crear", CategoriasRequestHandler.Crear);
app.MapGet("/categorias/listar", CategoriasRequestHandler.Listar);
app.MapPost("/lenguaje/crear", LenguajeRequestHandler.CrearRegistro);
app.MapDelete("/lenguaje/{id}", LenguajeRequestHandler.Eliminar);
app.MapGet("/lenguaje/{idCategoria}", LenguajeRequestHandler.ListarRegistros);
app.MapGet("/lenguaje/buscar", LenguajeRequestHandler.Buscar);

app.Run();