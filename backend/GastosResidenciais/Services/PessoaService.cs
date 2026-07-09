using GastosResidenciais.Data;
using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Mappers;
using GastosResidenciais.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Services;

/// <summary>
/// Responsável por gerenciar as pessoas cadastradas no sistema
/// e suas respectivas transações financeiras.
/// </summary>
public class PessoaService : IPessoaService
{
    private readonly AppDbContext _context;

    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cadastra uma nova pessoa no sistema.
    /// </summary>
    /// <param name="request">Dados informados para o cadastro da pessoa.</param>
    /// <returns>Os dados da pessoa recém-criada.</returns>
    public async Task<PessoaResponse> CriarAsync(CriarPessoaRequest request)
    {
        var pessoa = PessoaMapper.ToEntity(request);

        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();

        return PessoaMapper.ToDto(pessoa);
    }

    /// <summary>
    /// Retorna todas as pessoas cadastradas no sistema.
    /// </summary>
    public async Task<IEnumerable<PessoaResponse>> ObterTodasAsync()
    {
        var pessoas = await _context.Pessoas.ToListAsync();

        return pessoas.Select(PessoaMapper.ToDto);
    }

    /// <summary>
    /// Busca uma pessoa específica pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando não existe pessoa cadastrada com o id informado.
    /// </exception>
    public async Task<PessoaResponse> ObterPorIdAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id)
                     ?? throw new KeyNotFoundException($"Pessoa '{id}' não encontrada.");

        return PessoaMapper.ToDto(pessoa);
    }

    /// <summary>
    /// Retorna todas as transações financeiras associadas a uma pessoa.
    /// Antes de buscar as transações, garante que a pessoa realmente existe,
    /// evitando retornar uma lista vazia para um id inválido.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando não existe pessoa cadastrada com o id informado.
    /// </exception>
    public async Task<IEnumerable<TransacaoResponse>> ObterTransacoesPorPessoaAsync(Guid id)
    {
        var pessoaExiste = await _context.Pessoas.AnyAsync(p => p.Id == id);

        if (!pessoaExiste)
            throw new KeyNotFoundException($"Pessoa '{id}' não encontrada.");

        var transacoes = await _context.Transacoes
            .Where(t => t.PessoaId == id)
            .ToListAsync();

        return transacoes.Select(TransacaoMapper.ToDto);
    }

    /// <summary>
    /// Remove uma pessoa do sistema. Só é possível remover uma pessoa
    /// que já esteja cadastrada.
    /// </summary>
    /// <param name="id">Identificador da pessoa a ser removida.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando não existe pessoa cadastrada com o id informado.
    /// </exception>
    public async Task RemoverAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id)
                     ?? throw new KeyNotFoundException($"Pessoa '{id}' não encontrada.");

        _context.Pessoas.Remove(pessoa);

        await _context.SaveChangesAsync();
    }
}