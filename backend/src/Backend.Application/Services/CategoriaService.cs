using Backend.Application.Contracts.Repositories;
using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Backend.Application.Exceptions;
using Backend.Domain.Entities;
using Backend.Domain.Enums;

namespace Backend.Application.Services;

public class CategoriaService(ICategoriaRepo repo) : ICategoriaService
{
    // Método assíncrono para criar uma nova categoria no sistema
    // Recebe um DTO de requisição contendo os dados básicos da categoria
    // Retorna um DTO de resposta com os dados da categoria criada incluindo o ID gerado
    public async Task<CategoriaResponseDto> CriarCategoria(CriarCategoriaRequestDto requestDto)
    {
        // Verifica se já existe uma categoria com a descrição fornecida, caso exista, lança uma exceção.
        var categoriaExistente = await repo.ObterCategoriaPorDescricao(requestDto.Descricao);
        if (categoriaExistente is not null)
        {
            throw new RecursoJaExisteException("Já existe uma categoria com a descrição fornecida.");
        }

        // Mapeia o DTO de requisição para a entidade de domínio Categoria
        // A conversão da string "Finalidade" para o enum TipoTransacao é feita aqui
        var categoria = new Categoria
        {
            Descricao = requestDto.Descricao, // Descrição fornecida na requisição
            Finalidade = Enum.Parse<TipoTransacao>(requestDto.Finalidade) // Converte string para enum
        };

        // Salva a categoria no repositório (banco de dados)
        await repo.CriarCategoria(categoria);

        // 3. RETORNO DA RESPOSTA:
        // Converte a entidade salva em DTO de resposta para retornar ao cliente
        return new CategoriaResponseDto(categoria.Id, categoria.Descricao, categoria.Finalidade.ToString());
    }

    // Método assíncrono para obter todas as categorias cadastradas no sistema
    // Retorna uma coleção de DTOs de resposta, nunca retorna null
    public async Task<ICollection<CategoriaResponseDto>> ObterCategorias()
    {
        // 1. OBTER DADOS DO REPOSITÓRIO:
        // Busca todas as categorias do banco de dados através do repositório
        var categorias = await repo.ListarCategorias();

        // 2. MAPEAMENTO PARA DTO:
        // Converte cada entidade Categoria em um DTO de resposta
        // Inclui conversão do enum Finalidade para string
        return categorias.Select(categoria =>
            new CategoriaResponseDto(categoria.Id, categoria.Descricao, categoria.Finalidade.ToString())).ToList();
    }

    // Método assíncrono para obter uma categoria específica pelo seu ID
    // Recebe o GUID da categoria a ser buscada
    // Retorna um DTO de resposta ou lança exceção se não encontrada
    public async Task<CategoriaResponseDto> ObterCategoriaPorId(Guid id)
    {
        // 1. BUSCA ESPECÍFICA NO REPOSITÓRIO:
        // Busca a categoria pelo ID fornecido
        var categoria = await repo.ObterCategoriaPorId(id);

        // 2. VALIDAÇÃO DE EXISTÊNCIA:
        // Verifica se a categoria foi encontrada
        if (categoria is null)
        {
            // Lança exceção personalizada com mensagem descritiva
            // O middleware global capturará e converterá em HTTP 404 (Not Found)
            throw new NotFoundException($"A categoria com ID de valor = '{id}' não foi encontrada.");
        }

        // 3. RETORNO DO DTO:
        // Converte a entidade em DTO de resposta
        return new CategoriaResponseDto(categoria.Id, categoria.Descricao, categoria.Finalidade.ToString());
    }
}