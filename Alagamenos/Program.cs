using Alagamenos.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AlagamenosDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("FiapOracleDb")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .AddXmlSerializerFormatters(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Alagamenos",
        Version = "v1",
        Description = "API para envio de alertas de alagamento para usuários."
    });
    opt.EnableAnnotations(); 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Alagamenos v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); 

Alagamenos.Controllers.AlertaEndpoints.Map(app);
Alagamenos.Controllers.BairroEndpoints.Map(app);
Alagamenos.Controllers.CidadeEndpoints.Map(app);
Alagamenos.Controllers.EnderecoEndpoints.Map(app);
Alagamenos.Controllers.EstadoEndpoints.Map(app);
Alagamenos.Controllers.RuaEndpoints.Map(app);
Alagamenos.Controllers.UsuarioAlertaEndpoints.Map(app);

app.Run();