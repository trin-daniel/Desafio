namespace Backend.Application.DTOs;

public record ResumoGeralDto(
    List<TotalPessoaDto> TotaisPorPessoa,
    decimal TotalGeralReceitas,
    decimal TotalGeralDespesas,
    decimal SaldoLiquido
);