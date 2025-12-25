using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs;

public record CriarPessoaRequestDto(
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")]
    [MaxLength(64, ErrorMessage = "O nome deve ter no máximo 64 caracteres.")]
    string Nome,
    [Range(1, 140, ErrorMessage = "A idade deve estar dentro dos limites permitidos.")]
    int Idade);