using Backend.Application.Contracts.Repositories;
using Backend.Application.Contracts.Services;
using Backend.Application.DTOs;
using Backend.Domain.Enums;

namespace Backend.Application.Services;

/**
 * A responsabilidade desse serviço é calcular a receita e a despesa por pessoa, bem como a receita, a despesa e o saldo líquido de todas as pessoas envolvidas e, ao final, retornar uma coleção de objetos
 * populada com as receitas e despesas por pessoa, além da receita geral, despesa geral e saldo líquido de todos os envolvidos.
 */
public class RelatorioService(IPessoaRepo repo) : IRelatorioService
{
    public async Task<ResumoGeralDto> ObterResumoGeral()
    {
        var pessoas = await repo.ObterPessoasComTransacoes();
        var totaisPorPessoa = new List<TotalPessoaDto>();

        foreach (var pessoa in pessoas)
        {
            /*
             * Obtendo a soma de todas as receitas de uma pessoa, a cada iteração do foreach, uma pessoa diferente tem suas receitas calculadas.
             */
            var receitas = pessoa.Transacoes
                .Where(transacao => transacao.Tipo == TipoTransacao.Receita)
                .Sum(transacao => transacao.Valor);
            /*
             * Obtendo a soma de todas as despesas de uma pessoa, a cada iteração do foreach, uma pessoa diferente tem suas despesas calculadas.
             */
            var despesas = pessoa.Transacoes.Where(transacao => transacao.Tipo == TipoTransacao.Despesa)
                .Sum(transacao => transacao.Valor);

            /*
             * Calculando o saldo de uma pessoa, a cada iteração do foreach, uma pessoa diferente tem seu saldo calculado a partir da subtração de receitas por despesas.
             */
            var saldo = receitas - despesas;
            /*
             * A coleção totaisPorPessoa é populada a cada iteração do foreach com uma instância de TotalPessoaDto, que, por sua vez, é populada com o id, nome, receitas, despesas e saldo da pessoa da vez
             */
            totaisPorPessoa.Add(new TotalPessoaDto(pessoa.Id, pessoa.Nome, receitas, despesas, saldo));
        }

        /*
         * Aqui calculamos as receitas e despesas gerais, em outras palavras ocorre a soma de entradas e saidas de todas as pessoas envolvidas.
         */
        var totalGeralReceitas = totaisPorPessoa.Select(total => total.TotalReceitas).Sum();
        var totalGeralDespesas = totaisPorPessoa.Select(total => total.TotalDespesas).Sum();

        /*
         * A obtenção do valor do saldo líquido dá-se pela subtração do total geral de receitas pelo total geral de despesas.
         * Isso é necessário para saber quanto sobrou em caixa após todas as entradas e saídas de pessoas distintas.
         */
        var saldoLiquida = totalGeralReceitas - totalGeralDespesas;
        return new ResumoGeralDto(totaisPorPessoa, totalGeralReceitas, totalGeralDespesas, saldoLiquida);
    }
}