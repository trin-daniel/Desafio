using Backend.Application.DTOs;

namespace Backend.Application.Contracts.Services;

public interface ICategoriaService
{
    Task<CategoriaResponseDto> CriarCategoria(CriarCategoriaRequestDto requestDto);
    Task<ICollection<CategoriaResponseDto>> ObterCategorias();
    Task<CategoriaResponseDto> ObterCategoriaPorId(Guid id);
}