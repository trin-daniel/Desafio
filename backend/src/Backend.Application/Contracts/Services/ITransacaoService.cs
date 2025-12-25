using Backend.Application.DTOs;

namespace Backend.Application.Contracts.Services;

public interface ITransacaoService
{
    Task<TransacaoResponseDto> CriarTransacao(CriarTransacaoRequestDto request);
    Task<ICollection<TransacaoResponseDto>> ObterTransacoes();
    Task<TransacaoResponseDto> ObterTransacaoPorId(Guid id);
}