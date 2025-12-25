using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers;

[ApiController]
[Route("api/v1/categorias")]
public class CategoriaController(ICategoriaService service) : ControllerBase
{
    // Endpoint HTTP POST para criação de uma nova categoria
    // Rota completa: POST api/v1/categorias
    // Recebe os dados da categoria no corpo da requisição como JSON/XML
    // Retorna a categoria criada ou códigos de erro automáticos (400, 500, etc.)
    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> CriarCategoria([FromBody] CriarCategoriaRequestDto request)
    {
        // Delegar a lógica de criação para a camada de serviço
        var categoria = await service.CriarCategoria(request);

        // Retorna HTTP 201 (Created) com:
        // - Header Location apontando para o endpoint que obtém a categoria criada
        // - Corpo da resposta contém os dados da categoria criada (CategoriaResponseDto)
        return CreatedAtAction(nameof(ObterCategoriaPorId), new { categoria.Id }, categoria);
    }

    // Endpoint HTTP GET para obter todas as categorias
    // Rota completa: GET api/v1/categorias
    // Retorna lista de categorias ou 204 se não houver dados
    [HttpGet]
    public async Task<ActionResult<ICollection<CategoriaResponseDto>>> ObterCategorias()
    {
        // Solicita ao serviço todas as categorias cadastradas
        var categorias = await service.ObterCategorias();

        // Se não existirem categorias, retorna HTTP 204 (No Content)
        if (categorias.Count == 0)
        {
            return NoContent();
        }

        // Retorna HTTP 200 (OK) com a lista de categorias no corpo da resposta
        return Ok(categorias);
    }

    // Endpoint HTTP GET para obter uma categoria específica pelo ID
    // Rota completa: GET api/v1/categorias/{id}
    // O parâmetro {id:guid} na rota valida que o ID deve ser um GUID válido
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoriaResponseDto>> ObterCategoriaPorId(Guid id)
    {
        // Solicita ao serviço a categoria com o ID especificado
        var dto = await service.ObterCategoriaPorId(id);

        // Retorna HTTP 200 (OK) com os dados da categoria encontrada
        // Se a categoria não existir, o serviço lançará uma exceção
        // que será convertida em erro HTTP 404 pelo middleware de tratamento de exceções
        return Ok(dto);
    }
}