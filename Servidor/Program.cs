using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Servidor.Data;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection; // Para obter o método CreateScope
using FluentValidation.AspNetCore;
using Servidor.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurando o uso do SQLite
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurando o FluentValidation para validar automaticamente as ações do controlador.
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

// Configurando os serviços DocumentoService e COntratoService
builder.Services.AddScoped<IContratoDocumentoService, ContratoDocumentoService>();

// Configurando o serviço de LOGS
builder.Services.AddLogging();


// Configurando o Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiContratos", Version = "v1" });
});

var app = builder.Build();

// Escopo de serviço para obter uma instância de AppDbContext usando injeção de dependência
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // Garante que um banco de dados SQLite seja criado caso não exista.
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiContratos V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
