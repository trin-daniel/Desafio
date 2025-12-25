using Backend.Application.Contracts.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories;

public class PessoaRepo(AppDbContext context) : IPessoaRepo
{
    // Método assíncrono para criar uma nova pessoa no banco de dados
    // Recebe uma entidade Pessoa já preenchida com os dados necessários
    // Retorna a mesma entidade após a persistência.
    public async Task<Pessoa> CriarPessoa(Pessoa pessoa)
    {
        // Adiciona a entidade ao contexto do Entity Framework (tracking)
        await context.AddAsync(pessoa);
        // Persiste todas as mudanças pendentes no banco de dados
        await context.SaveChangesAsync();
        // Retorna a entidade.
        return pessoa;
    }

    // Método assíncrono para obter todas as pessoas do banco de dados
    // Retorna uma coleção de entidades Pessoa com o rastreamento do entity framework desabilitado.
    public async Task<ICollection<Pessoa>> ObterPessoas()
    {
        // AsNoTracking(): Desativa o tracking de mudanças - melhora desempenho para consultas somente leitura
        // ToListAsync(): Executa a consulta assincronamente e retorna lista materializada
        return await context.Pessoas.AsNoTracking().ToListAsync();
    }

    // Método assíncrono para obter todas as pessoas com suas transações relacionadas
    // Retorna uma coleção de pessoas, cada uma com sua coleção de transações carregada
    public async Task<ICollection<Pessoa>> ObterPessoasComTransacoes()
    {
        // Include(pessoa => pessoa.Transacoes): Carrega as transações relacionadas em uma única consulta (JOIN)
        // AsNoTracking(): Modo somente leitura para melhor performance
        return await context.Pessoas.AsNoTracking().Include(pessoa => pessoa.Transacoes).ToListAsync();
    }

    // Método assíncrono para buscar uma pessoa pelo nome exato
    // Retorna a primeira pessoa com o nome especificado ou null se não encontrada
    public async Task<Pessoa?> ObterPessoaPorNome(string nome)
    {
        // FirstOrDefaultAsync: Retorna o primeiro registro que atende à condição ou null
        var pessoa = await context.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.Nome == nome);
        return pessoa;
    }

    // Método assíncrono para remover uma pessoa do banco de dados
    // Recebe uma entidade Pessoa já carregada no contexto
    // Não retorna valor, apenas remove e persiste a mudança
    public async Task RemoverPessoa(Pessoa pessoa)
    {
        // Marca a entidade para remoção no contexto do Entity Framework
        context.Remove(pessoa);
        // Persiste a remoção no banco de dados
        await context.SaveChangesAsync();
    }

    // Método assíncrono para obter uma pessoa pelo seu ID
    // Retorna a entidade Pessoa correspondente ou null se não encontrada
    public async Task<Pessoa?> ObterPessoaPorId(Guid id)
    {
        return await context.Pessoas.AsNoTracking().FirstOrDefaultAsync(pessoa => pessoa.Id == id);
    }
}