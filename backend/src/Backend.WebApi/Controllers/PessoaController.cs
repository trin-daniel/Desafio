using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers;

[ApiController]
[Route("api/v1/pessoas")]
public class PessoaController(IPessoaService service) : ControllerBase
{
    // Endpoint HTTP POST para criação de uma nova pessoa
    // Decorado com [HttpPost] para indicar que responde a requisições POST
    // Retorna um ActionResult contendo PessoaResponseDto ou um código de erro
    // Método assíncrono que recebe os dados da pessoa via corpo da requisição ([FromBody])
    [HttpPost]
    public async Task<ActionResult<PessoaResponseDto>> CriarPessoa([FromBody] CriarPessoaRequestDto request)
    {
        // Chama o serviço para criar a pessoa no sistema (banco de dados, etc.)
        var pessoa = await service.CriarPessoa(request);

        // Retorna resposta HTTP 201 (Created) com:
        // - Location header apontando para o endpoint de consulta da pessoa criada
        // - Corpo da resposta contendo os dados da pessoa criada
        return CreatedAtAction(nameof(ObterPessoaPorId), new { id = pessoa.Id }, pessoa);
    }

    // Endpoint HTTP GET para listar todas as pessoas
    // Decorado com [HttpGet] sem parâmetros na rota
    // Retorna lista de PessoaResponseDto ou código de status apropriado
    [HttpGet]
    public async Task<ActionResult<ICollection<PessoaResponseDto>>> ListarPessoas()
    {
        // Solicita ao serviço a lista completa de pessoas cadastradas
        var pessoas = await service.ListarPessoas();

        // Se não houver pessoas cadastradas, retorna HTTP 204 (No Content)
        if (pessoas.Count == 0)
        {
            return NoContent();
        }

        // Retorna HTTP 200 (OK) com a lista de pessoas no corpo da resposta
        return Ok(pessoas);
    }

    // Endpoint HTTP GET para buscar uma pessoa específica por ID
    // Decorado com [HttpGet("{id:guid}"] - espera um GUID na rota
    // Exemplo: GET /api/pessoas/12345678-1234-1234-1234-123456789abc
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ICollection<PessoaResponseDto>>> ObterPessoaPorId([FromRoute] Guid id)
    {
        // Solicita ao serviço buscar a pessoa pelo ID fornecido na rota
        var pessoas = await service.ObterPessoaPorId(id);

        // Retorna HTTP 200 (OK) com os dados da pessoa encontrada
        return Ok(pessoas);
    }

    // Endpoint HTTP DELETE para remover uma pessoa por ID
    // Decorado com [HttpDelete("{id:guid}"] - espera um GUID na rota
    // Retorna apenas código de status (sem corpo na resposta)
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletarPessoa([FromRoute] Guid id)
    {
        // Solicita ao serviço remover a pessoa com o ID fornecido
        await service.RemoverPessoa(id);

        // Retorna HTTP 204 (No Content) indicando sucesso na exclusão
        return NoContent();
    }
}