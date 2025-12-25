using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs;

// Por que required duas vezes em algumas propriedades?
public record CriarTransacaoRequestDto
{
    private const double MinValue = 0.00d;
    private const double MaxValue = 99999.99d;
    private const int MinLength = 4;
    private const int MaxLength = 64;

    [Required(ErrorMessage = "A descrição é obrigatorio.")]
    [MinLength(MinLength, ErrorMessage = "A descrição da transação deve ter pelo menos 4 caracteres.")]
    [MaxLength(MaxLength, ErrorMessage = "A descrição da transação deve ter no máximo 64 caracteres.")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "O valor é obrigatorio.")]
    [Range(MinValue, MaxValue, ErrorMessage = "O valor esta fora dos limites permitidos.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo de transação deve ser especificado.")]
    [RegularExpression("(Despesa|Receita|Ambos)")]
    public required string Tipo { get; set; }

    [Required] public required Guid CategoriaId { get; set; }
    [Required] public required Guid PessoaId { get; set; }
}