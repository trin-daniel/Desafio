namespace Backend.Domain.Exceptions;

public sealed class TransacaoReceitaMenorDeIdadeNaoPermitidaException()
    : DominioException("Pessoas com menos de 18 anos não podem registrar transações do tipo receita.");