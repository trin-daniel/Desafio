using Backend.Domain.Entities;

namespace Backend.Application.Contracts.Repositories;

public interface ITransacaoRepo
{
    Task<Transacao> CriarTransacao(Transacao transacao);
    Task<ICollection<Transacao>> ObterTransacoes();
    Task<Transacao?> ObterTransacaoPorId(Guid id);
}