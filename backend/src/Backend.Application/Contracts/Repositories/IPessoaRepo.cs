using Backend.Domain.Entities;

namespace Backend.Application.Contracts.Repositories;

public interface IPessoaRepo
{
    Task<Pessoa> CriarPessoa(Pessoa request);
    Task<ICollection<Pessoa>> ObterPessoas();
    Task<ICollection<Pessoa>> ObterPessoasComTransacoes();
    Task RemoverPessoa(Pessoa pessoa);
    Task<Pessoa?> ObterPessoaPorId(Guid id);
    Task<Pessoa?> ObterPessoaPorNome(string nome);
    
}