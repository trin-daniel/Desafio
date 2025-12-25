using Backend.Application.DTOs;

namespace Backend.Application.Contracts.Services;

public interface IRelatorioService
{
    Task<ResumoGeralDto> ObterResumoGeral();
}