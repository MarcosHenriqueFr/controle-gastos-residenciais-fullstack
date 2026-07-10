namespace GastosResidenciais.Dtos.Resumo;

/// <summary>
/// Resumo financeiro de uma pessoa específica: total de receitas, despesas
/// e o saldo resultante (receitas - despesas).
/// </summary>
public record PessoaResumoResponse(
    Guid PessoaId,
    string Nome,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo
);