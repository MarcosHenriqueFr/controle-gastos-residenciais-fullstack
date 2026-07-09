using GastosResidenciais.Data;
using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Mappers;
using GastosResidenciais.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Services;

public class PessoaService : IPessoaService
{
    private readonly AppDbContext _context;

    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PessoaResponse> CriarAsync(CriarPessoaRequest request)
    {
        var pessoa = PessoaMapper.ToEntity(request);

        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();

        return PessoaMapper.ToDto(pessoa);
    }

    public async Task<IEnumerable<PessoaResponse>> ObterTodasAsync()
    {
        var pessoas = await _context.Pessoas.ToListAsync();

        return pessoas.Select(PessoaMapper.ToDto);
    }

    public async Task<PessoaResponse> ObterPorIdAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id)
                     ?? throw new KeyNotFoundException($"Pessoa '{id}' não encontrada.");

        return PessoaMapper.ToDto(pessoa);
    }

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

    public async Task RemoverAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id)
                     ?? throw new KeyNotFoundException($"Pessoa '{id}' não encontrada.");

        _context.Pessoas.Remove(pessoa);

        await _context.SaveChangesAsync();
    }
}