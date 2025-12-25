using Backend.Domain.Entities;

namespace Backend.Application.Contracts.Repositories;

public interface ICategoriaRepo
{
    Task CriarCategoria(Categoria categoria);
    Task<ICollection<Categoria>> ListarCategorias();
    Task<Categoria?> ObterCategoriaPorId(Guid id);
    Task<Categoria?> ObterCategoriaPorDescricao(string descricao);
}