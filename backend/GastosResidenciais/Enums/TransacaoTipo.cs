using System.Text.Json.Serialization;

namespace GastosResidenciais.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransacaoTipo
{
    Despesa = 0, 
    Receita = 1
}