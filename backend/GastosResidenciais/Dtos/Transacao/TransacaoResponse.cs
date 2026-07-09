using GastosResidenciais.Enums;

namespace GastosResidenciais.Dtos.Transacao;

public record TransacaoResponse(
    Guid Id,
    string Descricao,
    decimal Valor, 
    TransacaoTipo Tipo,
    Guid PessoaId
);