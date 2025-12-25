using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs;

/*
 * O CriarCategoriaRequestDto e um componente que e vinculao ao metodo CriarCategoria no controlador de categoria.
 * Sua responsabilidade e armazenar os dados da requisicao apos a validacao automatica dos dados, que e realizada pelo "asp.net core", durante o model bind do JSON enviado na requisicao com o modelo de entrada
 */
public record CriarCategoriaRequestDto(
    [Required(ErrorMessage = "A descrição da categoria é obrigatoria.")]
    [MinLength(4, ErrorMessage = "A descrição deve ter no mínimo 4 caracteres.")]
    [MaxLength(84, ErrorMessage = "A descricao deve possui no maximo 64 caracteres")]
    string Descricao,
    [Required(ErrorMessage = "A finalidade deve ser definida")]
    [RegularExpression(@"(Despesa|Receita)", ErrorMessage = "A finalidade dever ser: Despesa ou Receita")]
    string Finalidade);