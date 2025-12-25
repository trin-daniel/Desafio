namespace Backend.Application.DTOs;

/*
 * O componente CategoriaResponseDto e um objeto de transaferencia de dados entre as camadas,
 * sua responsabilidade e receber o valor das propriedades contidas na entidade retornada pelo repositorio
 * e trafegar esses dados para a camada e apresentacao (Web Api)
 */
public record CategoriaResponseDto(Guid Id, string Descricao, string Finalidade);