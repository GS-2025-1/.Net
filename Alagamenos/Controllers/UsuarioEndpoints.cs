using Alagamenos.DbConfig;
using Alagamenos.Model;
using Microsoft.EntityFrameworkCore;

namespace Alagamenos.Controllers;

public class UsuarioEndpoints
{
    public static void Map(WebApplication app)
    {

        var group = app.MapGroup("/usuarios").WithTags("Usuario");
        
        //Get all
        group.MapGet("/", async (AlagamenosDbContext db) =>
            await db.Usuarios.ToListAsync())
            .WithSummary("Retorna todos os usuarios")
            .WithDescription("Retorna todos os usuarios cadastrados no banco de dados, " +
                             "mesmo que só seja encontrado um usuario, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/{id}", async (int id, AlagamenosDbContext db) =>
        {
            var usuario = await db.Usuarios.FindAsync(id);
            return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
        })
        .WithSummary("Busca um usuario pelo ID")
        .WithDescription("Retorna os dados de um usuario específico com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/inserir", async (Usuario usuario, AlagamenosDbContext db) =>
            {
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return Results.Created($"/Usuarios/{usuario.Id}", usuario);
            })
            .WithSummary("Insere um novo usuario")
            .WithDescription("Adiciona um novo usuario ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/atualizar/{id}", async (int id, Usuario usuario, AlagamenosDbContext db) =>
        {
            var existing = await db.Usuarios.FindAsync(id);
            if (existing is null) return Results.NotFound();

            existing.Nome = usuario.Nome;
            existing.Email = usuario.Email;
            existing.DataNascimento = usuario.DataNascimento;
            existing.Senha = usuario.Senha;
            existing.Telefone = usuario.Telefone;
            
            await db.SaveChangesAsync();

            return Results.Ok($"Usuario com ID {id} atualizado com sucesso.");
        })
        .WithSummary("Atualiza um usuario existente")
        .WithDescription("Atualiza os dados de um usuario já cadastrado, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/deletar/{id}", async (int id, AlagamenosDbContext db) =>
            {
                var usuario = await db.Usuarios.FindAsync(id);
                if (usuario is null) return Results.NotFound();

                db.Usuarios.Remove(usuario);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Usuario com ID {id} removido com sucesso.");
            })
        .WithSummary("Remove um usuario")
        .WithDescription("Remove um usuario do banco de dados com base no ID informado. " +
                         "Caso o usuario não seja encontrado, retorna 404 Not Found.");
    }
}