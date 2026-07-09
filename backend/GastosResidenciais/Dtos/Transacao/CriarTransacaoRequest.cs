using GastosResidenciais.Enums;

namespace GastosResidenciais.Dtos.Transacao;

public record CriarTransacaoRequest(
    string Descricao,
    decimal Valor,
    TransacaoTipo Tipo,
    Guid PessoaId 
);