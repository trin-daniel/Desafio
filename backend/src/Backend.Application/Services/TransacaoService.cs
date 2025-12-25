using Backend.Application.Contracts.Repositories;
using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Backend.Application.Exceptions;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;

namespace Backend.Application.Services;

public class TransacaoService(ITransacaoRepo transacaoRepo, IPessoaRepo pessoaRepo, ICategoriaRepo categoriaRepo)
    : ITransacaoService
{
    public async Task<TransacaoResponseDto> CriarTransacao(CriarTransacaoRequestDto request)
    {
        // 1. Busca a pessoa pelo ID fornecido no DTO de requisição.
        var pessoa = await pessoaRepo.ObterPessoaPorId(request.PessoaId);

        // 2. Verifica se a pessoa foi encontrada. Caso contrário, lança uma exceção do tipo NotFoundException.
        if (pessoa is null)
        {
            throw new NotFoundException("A Pessoa informada não foi encontrada.");
        }

        // 3. Converte a string do campo Tipo (vinda do DTO) para o enum TipoTransacao.
        var tipoTransacao = Enum.Parse<TipoTransacao>(request.Tipo);

        // 4. Valida se a pessoa é menor de 18 anos e se o tipo de transação é Receita.
        //    Caso afirmativo, lança uma exceção específica (TransacaoReceitaMenorDeIdadeNaoPermitidaException).
        if (pessoa.Idade < 18 && tipoTransacao == TipoTransacao.Receita)
        {
            throw new TransacaoReceitaMenorDeIdadeNaoPermitidaException();
        }

        // 5. Busca a categoria pelo ID fornecido no DTO de requisição.
        var categoria = await categoriaRepo.ObterCategoriaPorId(request.CategoriaId);

        // 6. Verifica se a categoria foi encontrada. Caso contrário, lança uma exceção do tipo NotFoundException.
        if (categoria is null)
        {
            throw new NotFoundException("A categoria informada não foi encontrada.");
        }
        
        // 7. Verifica se a finalidade da categoria (que também é um TipoTransacao) corresponde ao tipo da transação.
        //    Caso não corresponda, lança uma exceção (ViolacaoTransacaoException).
        if (categoria != null && 
            categoria.Finalidade != tipoTransacao && 
            tipoTransacao != TipoTransacao.Ambos)
        {
            throw new ViolacaoTransacaoException();
        }
        
        // 8. Cria um novo objeto Transacao (entidade) com os dados fornecidos e obtidos.
        var transacao = new Transacao
        {
            Descricao = request.Descricao,
            Valor = request.Valor,
            Tipo = tipoTransacao,
            PessoaId = pessoa.Id,
            CategoriaId = request.CategoriaId
        };

        // 9. Persiste a transação no repositório (banco de dados).
        await transacaoRepo.CriarTransacao(transacao);

        // 10. Retorna um DTO de resposta com os dados da transação criada, incluindo informações adicionais
        //     como a descrição da categoria e o nome da pessoa (obtidos das entidades relacionadas).
        return new TransacaoResponseDto(
            transacao.Id,
            transacao.Descricao,
            transacao.Valor,
            transacao.Tipo.ToString(),
            transacao.CategoriaId,
            categoria.Descricao,
            transacao.PessoaId,
            pessoa.Nome
        );
    }
    
    // Método assíncrono para obter todas as transações cadastradas no sistema
    // Retorna uma coleção de DTOs de resposta contendo dados completos das transações
    // Inclui informações relacionadas de categoria e pessoa (via navegação)
    public async Task<ICollection<TransacaoResponseDto>> ObterTransacoes()
    {
        // 1. OBTER DADOS DO REPOSITÓRIO:
        // Busca todas as transações do banco de dados através do repositório
        // Presume-se que o método inclui propriedades de navegação (Categoria, Pessoa)
        // para evitar consultas adicionais (N+1) durante o mapeamento
        var transacoes = await transacaoRepo.ObterTransacoes();
        
        // 2. MAPEAMENTO COMPLETO PARA DTO:
        // Para cada transação encontrada, cria um DTO de resposta enriquecido
        // O mapeamento inclui dados da própria transação e informações relacionadas:
        return transacoes.Select(transacao => new TransacaoResponseDto(
            transacao.Id,                       // ID único da transação
            transacao.Descricao,                // Descrição da transação
            transacao.Valor,                    // Valor monetário
            transacao.Tipo.ToString(),          // Tipo convertido para string (ex: "Receita")
            transacao.CategoriaId,              // ID da categoria associada
            transacao.Categoria.Descricao,      // Descrição da categoria (via navegação)
            transacao.PessoaId,                 // ID da pessoa associada
            transacao.Pessoa.Nome               // Nome da pessoa (via navegação)
        )).ToList(); // Materializa a coleção em lista
    }

    // Método assíncrono para obter uma transação específica pelo seu ID
    // Recebe o GUID da transação a ser buscada
    // Retorna um DTO de resposta completo ou lança exceção se não encontrada
    public async Task<TransacaoResponseDto> ObterTransacaoPorId(Guid id)
    {
        // Busca a transação pelo ID fornecido
        // O método inclui propriedades de navegação (Categoria, Pessoa)
        var transacao = await transacaoRepo.ObterTransacaoPorId(id);

        // Verifica se a transação foi encontrada
        if (transacao is null)
        {
            // Lança exceção personalizada com mensagem descritiva
            // O middleware global capturará e converterá em HTTP 404 (Not Found)
            throw new NotFoundException($"A transação com ID = {id} não foi encontrada.");
        }
        
        // Converte a entidade em DTO de resposta com dados completos
        // Inclui informações de categoria e pessoa para conveniência do cliente
        return new TransacaoResponseDto(
            transacao.Id,
            transacao.Descricao,
            transacao.Valor,
            transacao.Tipo.ToString(),
            transacao.CategoriaId,
            transacao.Categoria.Descricao,
            transacao.PessoaId,
            transacao.Pessoa.Nome
        );
    }
}