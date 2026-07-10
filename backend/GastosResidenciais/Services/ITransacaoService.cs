using GastosResidenciais.Dtos.Transacao;

namespace GastosResidenciais.Services;

public interface ITransacaoService
{
    Task<TransacaoResponse> CriarAsync(CriarTransacaoRequest request);
    Task<IEnumerable<TransacaoResponse>> ListarTodosAsync();
    Task<TransacaoResponse> ObterPorIdAsync(Guid id);
}