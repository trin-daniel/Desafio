using Backend.Application.Contracts.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories;

public class CategoriaRepo(AppDbContext context) : ICategoriaRepo
{
    // Método assíncrono para criar e salvar uma nova categoria no banco de dados.
    // Recebe uma entidade Categoria já preenchida e a adiciona ao DbSet correspondente.
    // Utiliza AddAsync para operação assíncrona de adição e SaveChangesAsync para persistir.
    public async Task CriarCategoria(Categoria categoria)
    {
        await context.Categorias.AddAsync(categoria);
        await context.SaveChangesAsync();
    }

    // Método assíncrono para listar todas as categorias do banco de dados.
    // Retorna uma coleção de todas as entidades Categoria existentes.
    // Utiliza ToListAsync para executar a consulta de forma assíncrona e obter a lista.
    public async Task<ICollection<Categoria>> ListarCategorias()
    {
        return await context.Categorias.ToListAsync();
    }

    // Método assíncrono para obter uma categoria específica pelo seu ID.
    // Retorna a primeira categoria que corresponde ao ID fornecido, ou null se não encontrada.
    // Utiliza FirstOrDefaultAsync para executar a consulta de forma assíncrona.
    public async Task<Categoria?> ObterCategoriaPorId(Guid id)
    {
        return await context.Categorias.FirstOrDefaultAsync(categoria => categoria.Id == id);
    }

    // Método assíncrono para obter uma categoria pela descrição.
    // Retorna a primeira categoria que tem a descrição exatamente igual à fornecida, ou null se não encontrada.
    // Utiliza FirstOrDefaultAsync para executar a consulta de forma assíncrona.
    public async Task<Categoria?> ObterCategoriaPorDescricao(string descricao)
    {
        return await context.Categorias.FirstOrDefaultAsync(categoria => categoria.Descricao == descricao);
    }
}