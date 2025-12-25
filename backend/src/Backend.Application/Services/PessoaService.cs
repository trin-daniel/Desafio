using Backend.Application.Contracts.Repositories;
using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Backend.Application.Exceptions;
using Backend.Domain.Entities;

namespace Backend.Application.Services;

public class PessoaService(IPessoaRepo repo) : IPessoaService
{
    // Método assíncrono para criar uma nova pessoa.
    // Recebe um DTO de requisição (CriarPessoaRequestDto) com os dados necessários para criar uma pessoa.
    // Retorna um DTO de resposta (PessoaResponseDto) contendo os dados da pessoa criada.
    public async Task<PessoaResponseDto> CriarPessoa(CriarPessoaRequestDto request)
    {
        // Verifica se já existe uma pessoa com o mesmo nome no repositório.
        var pessoaJaExiste = await repo.ObterPessoaPorNome(request.Nome);
        if (pessoaJaExiste is not null)
        {
            // Se a pessoa já existe, lança uma exceção personalizada.
            throw new RecursoJaExisteException("A pessoa que você está tentando cadastrar já existe.");
        }

        // Cria uma nova instância da entidade Pessoa com os dados fornecidos no request.
        var pessoa = new Pessoa
        {
            Nome = request.Nome,
            Idade = request.Idade
        };

        // Persiste a pessoa no repositório e obtém a entidade salva (com o ID gerado, por exemplo).
        var pessoaCriada = await repo.CriarPessoa(pessoa);

        // Retorna um DTO de resposta com os dados da pessoa criada.
        return new PessoaResponseDto(pessoaCriada.Id, pessoaCriada.Nome, pessoaCriada.Idade);
    }

    // Método assíncrono para listar todas as pessoas.
    // Retorna uma coleção de DTOs de resposta (PessoaResponseDto).
    public async Task<ICollection<PessoaResponseDto>> ListarPessoas()
    {
        // Obtém todas as pessoas do repositório.
        var pessoas = await repo.ObterPessoas();

        // Converte cada entidade Pessoa em um PessoaResponseDto e retorna a lista.
        return pessoas.Select(pessoa => new PessoaResponseDto(pessoa.Id, pessoa.Nome, pessoa.Idade)).ToList();
    }

    // Método assíncrono para remover uma pessoa pelo seu ID.
    // Não retorna valor (void) porque a operação é de remoção.
    public async Task RemoverPessoa(Guid id)
    {
        // Tenta obter a pessoa pelo ID fornecido.
        var pessoa = await repo.ObterPessoaPorId(id);
        if (pessoa is null)
        {
            // Se a pessoa não for encontrada, lança uma exceção NotFoundException.
            throw new NotFoundException($"A pessoa de ID = {id} não foi encontrada.");
        }

        // Remove a pessoa do repositório.
        await repo.RemoverPessoa(pessoa);
    }

    // Método assíncrono para obter uma pessoa pelo seu ID.
    // Retorna um DTO de resposta (PessoaResponseDto) com os dados da pessoa.
    public async Task<PessoaResponseDto> ObterPessoaPorId(Guid id)
    {
        // Tenta obter a pessoa pelo ID fornecido.
        var pessoa = await repo.ObterPessoaPorId(id);

        // Se a pessoa não for encontrada, lança uma exceção NotFoundException.
        // Caso contrário, retorna um DTO com os dados da pessoa.
        return pessoa is null
            ? throw new NotFoundException($"A pessoa de ID = {id} não foi encontrada.")
            : new PessoaResponseDto(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }
}