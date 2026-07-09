using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Models;

namespace GastosResidenciais.Services;

public interface ITransacaoService
{
    Task<TransacaoResponse> CriarAsync(CriarTransacaoRequest request);
    Task<IEnumerable<TransacaoResponse>> ListarTodosAsync();
    Task<TransacaoResponse> ObterPorIdAsync(Guid id);
}