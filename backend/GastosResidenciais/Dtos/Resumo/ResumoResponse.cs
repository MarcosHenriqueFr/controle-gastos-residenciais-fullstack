namespace GastosResidenciais.Dtos.Resumo;

/// <summary>
/// Totais consolidados de receitas, despesas e saldo.
/// Usado tanto para o resumo de uma pessoa quanto para o total geral do sistema.
/// </summary>
public record ResumoResponse(
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo
);