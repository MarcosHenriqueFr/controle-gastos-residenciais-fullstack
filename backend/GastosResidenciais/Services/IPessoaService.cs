using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Models;

namespace GastosResidenciais.Services;

public interface IPessoaService
{
    PessoaResponse Cadastrar(CriarPessoaRequest pessoa);
    IEnumerable<PessoaResponse> ListarPessoas();
    PessoaResponse ObterPessoaPorId(Guid id);
    IEnumerable<TransacaoResponse> ListarTransacoesPorPessoa(Guid id);
    void ExcluirPessoa(Guid id);
}