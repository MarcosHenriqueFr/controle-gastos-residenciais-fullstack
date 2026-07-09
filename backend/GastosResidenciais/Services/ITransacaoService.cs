using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Models;

namespace GastosResidenciais.Services;

public interface ITransacaoService
{
    TransacaoResponse Criar(CriarTransacaoRequest transacao);
    IEnumerable<Transacao> ListarTodasTransacoes();
    TransacaoResponse ObterTransacaoPorId(Guid id);
}