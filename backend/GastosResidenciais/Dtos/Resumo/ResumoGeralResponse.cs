namespace GastosResidenciais.Dtos.Resumo;

/// <summary>
/// Resultado completo do resumo financeiro do sistema: o detalhamento
/// por pessoa e o total geral somando todas elas.
/// </summary>
public record ResumoGeralResponse(
    IEnumerable<PessoaResumoResponse> Pessoas,
    ResumoResponse TotalGeral
);