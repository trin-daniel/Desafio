using Backend.Application.Contracts.Repositories;
using Backend.Application.Contracts.Services;
using Backend.Application.Services;
using Backend.Infrastructure.Data.Context;
using Backend.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Extensions;

/*
 * Qual e a responsabilidade dessa extensao?
 *
 * 
 */

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Default")));
        services.AddScoped<ICategoriaRepo, CategoriaRepo>();
        services.AddScoped<IPessoaRepo, PessoaRepo>();
        services.AddScoped<ITransacaoRepo, TransacaoRepo>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();
        return services;
    }
}