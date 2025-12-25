namespace Backend.Application.DTOs;
public record TransacaoResponseDto(
    Guid Id,
    string Descricao,
    decimal Valor,
    string Tipo,
    Guid CategoriaId,
    string CategoriaNome,
    Guid PessoaId,
    string PessoaNome);