namespace Backend.Domain.Exceptions;

public sealed class ViolacaoTransacaoException(): DominioException("A finalidade da transação deve estar de acordo com a categoria.");