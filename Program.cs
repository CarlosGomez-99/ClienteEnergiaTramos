using System.Text.Json.Serialization;
using EnergiaElectrica;
using EnergiaElectrica.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<EnergiaContext>(builder.Configuration.GetConnectionString("cnEnergia")); //Aqui se debe colocar el nombre de la variable ConnectionStrings configurado en el archivo appsettings.json
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<ITramoServices, TramoServices>();
builder.Services.AddScoped<IClienteService, ClienteServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
