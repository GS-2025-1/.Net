using Alagamenos.DbConfig;
using Alagamenos.Model;
using Microsoft.EntityFrameworkCore;

namespace Alagamenos.Controllers;

public class AlertaEndpoints
{
    public static void Map(WebApplication app)
    {

        var group = app.MapGroup("/alertas").WithTags("Alerta");
        
        //Get all
        group.MapGet("/", async (AlagamenosDbContext db) =>
            await db.Alertas
                .Include(a => a.Rua)
                .ToListAsync())
            .WithSummary("Retorna todos os alertas")
            .WithDescription("Retorna todos os alertas cadastrados no banco de dados, " +
                             "mesmo que só seja encontrado um alerta, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/{id}", async (int id, AlagamenosDbContext db) =>
        {
            var alerta = await db.Alertas
                .Include(a => a.Rua)
                .FirstOrDefaultAsync(a => a.Id == id);
            return alerta is not null ? Results.Ok(alerta) : Results.NotFound();
        })
        .WithSummary("Busca um alerta pelo ID")
        .WithDescription("Retorna os dados de um alerta específico com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/inserir", async (Alerta alerta, AlagamenosDbContext db) =>
            {
                db.Alertas.Add(alerta);
                await db.SaveChangesAsync();
                return Results.Created($"/Alertas/{alerta.Id}", alerta);
            })
            .WithSummary("Insere um novo alerta")
            .WithDescription("Adiciona um novo alerta ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/atualizar/{id}", async (int id, Alerta alerta, AlagamenosDbContext db) =>
        {
            var existing = await db.Alertas.FindAsync(id);
            if (existing is null) return Results.NotFound();

            existing.Mensagem = alerta.Mensagem;
            existing.DataCriacao = alerta.DataCriacao;
            existing.RuaId = alerta.RuaId;
            await db.SaveChangesAsync();

            return Results.Ok($"Alerta com ID {id} atualizado com sucesso.");
        })
        .WithSummary("Atualiza um alerta existente")
        .WithDescription("Atualiza os dados de um alerta já cadastrado, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/deletar/{id}", async (int id, AlagamenosDbContext db) =>
            {
                var alerta = await db.Alertas.FindAsync(id);
                if (alerta is null) return Results.NotFound();

                db.Alertas.Remove(alerta);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Alerta com ID {id} removido com sucesso.");
            })
        .WithSummary("Remove um alerta")
        .WithDescription("Remove um alerta do banco de dados com base no ID informado. " +
                         "Caso o alerta não seja encontrado, retorna 404 Not Found.");
    }
}