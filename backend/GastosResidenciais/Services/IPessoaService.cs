using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Models;

namespace GastosResidenciais.Services;

public interface IPessoaService
{
    Task<PessoaResponse> CriarAsync(CriarPessoaRequest request);

    Task<IEnumerable<PessoaResponse>> ObterTodasAsync();

    Task<PessoaResponse> ObterPorIdAsync(Guid id);

    Task<IEnumerable<TransacaoResponse>> ObterTransacoesPorPessoaAsync(Guid id);

    Task RemoverAsync(Guid id);
}