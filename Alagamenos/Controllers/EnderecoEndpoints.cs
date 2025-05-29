using Alagamenos.DbConfig;
using Alagamenos.Model;
using Microsoft.EntityFrameworkCore;

namespace Alagamenos.Controllers;

public class EnderecoEndpoints
{
    public static void Map(WebApplication app)
    {

        var group = app.MapGroup("/enderecos").WithTags("Endereco");
        
        //Get all
        group.MapGet("/", async (AlagamenosDbContext db) =>
            await db.Enderecos
                .Include(e => e.Rua)
                .Include(e => e.Usuario)
                .ToListAsync())
            .WithSummary("Retorna todos os enderecos")
            .WithDescription("Retorna todos os endereco cadastrados no banco de dados, " +
                             "mesmo que só seja encontrado um endereco, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/{id}", async (int id, AlagamenosDbContext db) =>
        {
            var endereco = await db.Enderecos
                .Include(e => e.Rua)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(e => e.Id == id);
            return endereco is not null ? Results.Ok(endereco) : Results.NotFound();
        })
        .WithSummary("Busca um endereco pelo ID")
        .WithDescription("Retorna os dados de um endereco específico com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/inserir", async (Endereco endereco, AlagamenosDbContext db) =>
            {
                db.Enderecos.Add(endereco);
                await db.SaveChangesAsync();
                return Results.Created($"/Enderecos/{endereco.Id}", endereco);
            })
            .WithSummary("Insere um novo endereco")
            .WithDescription("Adiciona um novo endereco ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/atualizar/{id}", async (int id, Endereco endereco, AlagamenosDbContext db) =>
        {
            var existing = await db.Enderecos.FindAsync(id);
            if (existing is null) return Results.NotFound();

            existing.NumeroEndereco = endereco.NumeroEndereco;
            existing.Complemento = endereco.Complemento;
            existing.RuaId = endereco.RuaId;
            await db.SaveChangesAsync();

            return Results.Ok($"Endereco com ID {id} atualizado com sucesso.");
        })
        .WithSummary("Atualiza um endereco existente")
        .WithDescription("Atualiza os dados de um endereco já cadastrado, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/deletar/{id}", async (int id, AlagamenosDbContext db) =>
            {
                var endereco = await db.Enderecos.FindAsync(id);
                if (endereco is null) return Results.NotFound();

                db.Enderecos.Remove(endereco);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Endereco com ID {id} removido com sucesso.");
            })
        .WithSummary("Remove um endereco")
        .WithDescription("Remove um endereco do banco de dados com base no ID informado. " +
                         "Caso o endereco não seja encontrado, retorna 404 Not Found.");
    }
}