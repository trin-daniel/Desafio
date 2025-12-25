using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers;

[ApiController]
[Route("api/v1/relatorios")]
public class RelatorioController(IRelatorioService service) : ControllerBase
{
    // Endpoint HTTP GET para obter um resumo geral/estatísticas
    // Rota completa: GET api/v1/relatorios
    // Retorna um objeto TotalPessoaDto contendo dados consolidados
    [HttpGet]
    public async Task<ActionResult<TotalPessoaDto>> ObterResumoGeral()
    {
        // Delegar a lógica de cálculo/consolidação para a camada de serviço
        // O serviço acessa o repositório, faz cálculos e retorna DTO formatado
        var resumo = await service.ObterResumoGeral();

        // Retorna HTTP 200 (OK) com os dados do relatório no corpo da resposta
        return Ok(resumo);
    }
}