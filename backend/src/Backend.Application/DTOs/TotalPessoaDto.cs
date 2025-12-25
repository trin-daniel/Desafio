namespace Backend.Application.DTOs;

public record TotalPessoaDto(
    Guid PessoaId,
    string PessoaNome,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo
);