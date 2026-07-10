using GastosResidenciais.Data;
using GastosResidenciais.Dtos.Resumo;
using GastosResidenciais.Enums;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Services;

/// <summary>
/// Responsável por consolidar as transações financeiras de todas as pessoas
/// cadastradas, calculando o total de receitas, despesas e o saldo de cada
/// uma, além do total geral do sistema como um todo.
/// </summary>
public class ResumoService : IResumoService
{
    private readonly AppDbContext _context;

    public ResumoService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Monta o resumo financeiro completo: uma linha por pessoa cadastrada
    /// (mesmo que ela não tenha nenhuma transação, aparecendo com valores zerados)
    /// e, ao final, o total somando todas as pessoas.
    /// </summary>
    public async Task<ResumoGeralResponse> Resumir()
    {
        var pessoas = await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();

        var resumoPorPessoa = pessoas
            .Select(MontarResumoDaPessoa)
            .ToList();

        var totalGeral = MontarTotalGeral(resumoPorPessoa);

        return new ResumoGeralResponse(resumoPorPessoa, totalGeral);
    }

    /// <summary>
    /// Calcula o total de receitas, despesas e o saldo de uma pessoa,
    /// a partir de todas as transações associadas a ela.
    /// </summary>
    /// <param name="pessoa">Pessoa com suas transações já carregadas.</param>
    private static PessoaResumoResponse MontarResumoDaPessoa(Models.Pessoa pessoa)
    {
        var totalReceitas = pessoa.Transacoes
            .Where(t => t.Tipo == TransacaoTipo.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = pessoa.Transacoes
            .Where(t => t.Tipo == TransacaoTipo.Despesa)
            .Sum(t => t.Valor);

        var saldo = totalReceitas - totalDespesas;

        return new PessoaResumoResponse(pessoa.Id, pessoa.Nome, totalReceitas, totalDespesas, saldo);
    }

    /// <summary>
    /// Soma os resumos individuais de todas as pessoas para formar
    /// o total geral do sistema.
    /// </summary>
    /// <param name="resumoPorPessoa">Resumo já calculado de cada pessoa.</param>
    private static ResumoResponse MontarTotalGeral(IReadOnlyCollection<PessoaResumoResponse> resumoPorPessoa)
    {
        var totalReceitas = resumoPorPessoa.Sum(r => r.TotalReceitas);
        var totalDespesas = resumoPorPessoa.Sum(r => r.TotalDespesas);
        var saldo = totalReceitas - totalDespesas;

        return new ResumoResponse(totalReceitas, totalDespesas, saldo);
    }
}