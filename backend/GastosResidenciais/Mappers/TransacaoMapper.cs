using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Models;

namespace GastosResidenciais.Mappers;

public static class TransacaoMapper
{
    public static Transacao ToEntity(CriarTransacaoRequest request)
    {
        return new Transacao(request.Descricao, request.Valor, request.Tipo, request.PessoaId);
    }

    public static TransacaoResponse ToDto(Transacao transacao)
    {
        return new TransacaoResponse(transacao.Id, transacao.Descricao, transacao.Valor, transacao.Tipo, transacao.PessoaId);
    }
}