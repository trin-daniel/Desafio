using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers;

[ApiController]
[Route("api/v1/transacoes")]
public class TransacaoController(ITransacaoService service) : ControllerBase
{
    // Endpoint HTTP POST para criar uma nova transação
    // Rota completa: POST api/v1/transacoes
    // Recebe os dados da transação no corpo da requisição.
    // Retorna os dados da transação criada ou códigos de erro apropriados
    [HttpPost]
    public async Task<ActionResult<TransacaoResponseDto>> CriarTransacao([FromBody] CriarTransacaoRequestDto request)
    {
        // Delegar a lógica de criação da transação para a camada de serviço
        // O serviço deve validar regras de negócio, chamar o repositorio e retornar uma transação.
        var transacao = await service.CriarTransacao(request);

        // Retorna HTTP 201 (OK) com os dados da transação criada
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }

    // Endpoint HTTP GET para listar todas as transações
    // Rota completa: GET api/v1/transacoes
    // Retorna uma coleção de transações ou 204 se não houver dados
    [HttpGet]
    public async Task<ActionResult<ICollection<TransacaoResponseDto>>> ListarTransacoes()
    {
        // Solicita ao serviço todas as transações cadastradas
        var transacoes = await service.ObterTransacoes();

        // Se não houver transações, retorna HTTP 204 (No Content)
        if (transacoes.Count == 0)
        {
            return NoContent();
        }

        // Retorna HTTP 200 (OK) com a lista de transações no corpo da resposta
        return Ok(transacoes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransacaoResponseDto>> ObterTransacaoPorId(Guid id)
    {
        var transacao = await service.ObterTransacaoPorId(id);
        return Ok(transacao);
    }
}