using Alagamenos.DbConfig;
using Alagamenos.Model;
using Microsoft.EntityFrameworkCore;

namespace Alagamenos.Controllers;

public class RuaEndpoints
{
    public static void Map(WebApplication app)
    {

        var group = app.MapGroup("/ruas").WithTags("Rua");
        
        //Get all
        group.MapGet("/", async (AlagamenosDbContext db) =>
            await db.Ruas
                .Include(r => r.Bairro)
                .ToListAsync())
            .WithSummary("Retorna todos as ruas")
            .WithDescription("Retorna todos as ruas cadastradas no banco de dados, " +
                             "mesmo que só seja encontrado uma rua, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/{id}", async (int id, AlagamenosDbContext db) =>
        {
            var rua = await db.Ruas
                .Include(r => r.Bairro)
                .FirstOrDefaultAsync(r => r.Id == id);
            return rua is not null ? Results.Ok(rua) : Results.NotFound();
        })
        .WithSummary("Busca uma rua pelo ID")
        .WithDescription("Retorna os dados de uma rua específico com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/inserir", async (Rua rua, AlagamenosDbContext db) =>
            {
                db.Ruas.Add(rua);
                await db.SaveChangesAsync();
                return Results.Created($"/Ruas/{rua.Id}", rua);
            })
            .WithSummary("Insere uma nova rua")
            .WithDescription("Adiciona uma nova rua ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/atualizar/{id}", async (int id, Rua rua, AlagamenosDbContext db) =>
        {
            var existing = await db.Ruas.FindAsync(id);
            if (existing is null) return Results.NotFound();

            existing.NomeRua = rua.NomeRua;
            await db.SaveChangesAsync();

            return Results.Ok($"Rua com ID {id} atualizado com sucesso.");
        })
        .WithSummary("Atualiza uma rua existente")
        .WithDescription("Atualiza os dados de uma rua já cadastrado, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/deletar/{id}", async (int id, AlagamenosDbContext db) =>
            {
                var rua = await db.Ruas.FindAsync(id);
                if (rua is null) return Results.NotFound();

                db.Ruas.Remove(rua);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Rua com ID {id} removido com sucesso.");
            })
        .WithSummary("Remove uma rua")
        .WithDescription("Remove uma rua do banco de dados com base no ID informado. " +
                         "Caso a rua não seja encontrado, retorna 404 Not Found.");
    }
}