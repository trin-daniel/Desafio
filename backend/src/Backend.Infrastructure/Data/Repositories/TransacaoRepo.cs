using Backend.Application.Contracts.Repositories;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories;

public class TransacaoRepo(AppDbContext context) : ITransacaoRepo
{
    // Método assíncrono para criar uma nova transação no banco de dados.
    // Recebe uma entidade Transacao já preenchida.
    // Adiciona a transação ao DbSet Transacaos.
    // Salva as alterações no banco de dados de forma assíncrona.
    // Retorna a mesma entidade transação.
    public async Task<Transacao> CriarTransacao(Transacao transacao)
    {
        await context.Transacaos.AddAsync(transacao);
        await context.SaveChangesAsync();
        return transacao;
    }

    // Método assíncrono para obter todas as transações do banco de dados.
    // Retorna uma coleção de entidades Transacao, mas com as entidades relacionadas (Categoria e Pessoa) carregadas.
    // Isso é feito através dos métodos Include, que fazem join com as tabelas Categoria e Pessoa.
    // O método ToListAsync executa a consulta e retorna uma lista.
    public async Task<ICollection<Transacao>> ObterTransacoes()
    {
        return await context.Transacaos
            .Include(transacao => transacao.Categoria)   // Carrega a entidade Categoria relacionada
            .Include(transacao => transacao.Pessoa)      // Carrega a entidade Pessoa relacionada
            .ToListAsync();
    }

    // Método assíncrono para obter uma transação específica pelo seu ID.
    // Retorna a transação que possui o ID fornecido, ou null se não encontrada.
    // Assim como no método ObterTransacoes, também carrega as entidades relacionadas (Categoria e Pessoa) usando Include.
    // O método FirstOrDefaultAsync retorna o primeiro elemento que satisfaz a condição ou null.
    public async Task<Transacao?> ObterTransacaoPorId(Guid id)
    {
        return await context.Transacaos
            .Include(transacao => transacao.Categoria)
            .Include(transacao => transacao.Pessoa)
            .FirstOrDefaultAsync(transacao => transacao.Id == id);
    }
}