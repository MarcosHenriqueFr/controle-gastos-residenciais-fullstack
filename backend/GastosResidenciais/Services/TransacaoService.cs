using GastosResidenciais.Data;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Enums;
using GastosResidenciais.Mappers;
using GastosResidenciais.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Services;

/// <summary>
/// Responsável por gerenciar as transações financeiras cadastradas no sistema.
/// Aplica a regra de que pessoas menores de 18 anos só podem registrar
/// despesas, nunca receitas.
/// </summary>
public class TransacaoService : ITransacaoService
{

    private readonly AppDbContext _context;

    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cadastra uma nova transação financeira para uma pessoa.
    /// Antes de salvar, valida se a pessoa existe e se ela tem idade
    /// suficiente para o tipo de transação informado.
    /// </summary>
    /// <param name="request">Dados da transação a ser criada.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a pessoa informada não existe.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Lançada quando a pessoa é menor de idade e a transação não é uma despesa.
    /// </exception>
    public async Task<TransacaoResponse> CriarAsync(CriarTransacaoRequest request)
    {
        var pessoa = await _context.Pessoas.FindAsync(request.PessoaId)
                     ?? throw new KeyNotFoundException($"Pessoa '{request.PessoaId}' não encontrada.");

        ValidarPermissaoPorIdade(pessoa, request.Tipo);

        var transacao = TransacaoMapper.ToEntity(request);

        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();

        return TransacaoMapper.ToDto(transacao);
    }
    
    /// <summary>
    /// Garante que menores de idade só cadastrem despesas. Receitas
    /// (ex: salário, renda) exigem que a pessoa já seja maior de idade.
    /// </summary>
    /// <param name="pessoa">Pessoa dona da transação.</param>
    /// <param name="tipo">Tipo da transação sendo cadastrada.</param>
    private static void ValidarPermissaoPorIdade(Pessoa pessoa, TransacaoTipo tipo)
    {
        const int idadeMinimaParaReceita = 18;
        if (pessoa.Idade < idadeMinimaParaReceita && tipo != TransacaoTipo.Despesa)
        {
            throw new InvalidOperationException(
                $"Pessoa menor de {idadeMinimaParaReceita} anos só pode cadastrar transações do tipo despesa.");
        }
    }

    /// <summary>
    /// Retorna todas as transações cadastradas no sistema.
    /// </summary>
    public async Task<IEnumerable<TransacaoResponse>> ListarTodosAsync()
    {
        var transacoes = await _context.Transacoes.ToListAsync();

        return transacoes.Select(TransacaoMapper.ToDto);
    }

    /// <summary>
    /// Busca uma transação específica pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador da transação.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando não existe transação cadastrada com o id informado.
    /// </exception>
    public async Task<TransacaoResponse> ObterPorIdAsync(Guid id)
    {
        var transacao = await _context.Transacoes.FindAsync(id)
                         ?? throw new KeyNotFoundException($"Transação '{id}' não encontrada.");

        return TransacaoMapper.ToDto(transacao);
    }
}