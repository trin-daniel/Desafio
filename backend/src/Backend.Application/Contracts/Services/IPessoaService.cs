using Backend.Application.DTOs;

namespace Backend.Application.Contracts.Services;

public interface IPessoaService
{
    Task<PessoaResponseDto> CriarPessoa(CriarPessoaRequestDto request);
    Task<ICollection<PessoaResponseDto>> ListarPessoas();
    Task RemoverPessoa(Guid id);
    Task<PessoaResponseDto> ObterPessoaPorId(Guid id);
}