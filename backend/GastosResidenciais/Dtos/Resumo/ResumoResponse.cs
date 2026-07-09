namespace GastosResidenciais.Dtos.Resumo;

public record ResumoResponse(
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo
);