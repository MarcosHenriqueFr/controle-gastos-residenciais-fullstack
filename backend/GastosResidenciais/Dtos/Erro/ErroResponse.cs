namespace GastosResidenciais.Dtos.Erro;

public class ErroResponse
{
    public int StatusCode { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public string? Detalhes { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}