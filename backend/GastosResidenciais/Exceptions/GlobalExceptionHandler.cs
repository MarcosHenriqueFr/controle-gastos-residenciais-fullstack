using System.Net;
using System.Text.Json;
using GastosResidenciais.Dtos.Erro;

namespace GastosResidenciais.Exceptions;

/// <summary>
/// Captura qualquer exceção não tratada que ocorra durante o processamento
/// de uma requisição e converte essa exceção em uma resposta HTTP padronizada,
/// evitando que cada controller precise repetir try/catch para tratar erros
/// de negócio como "recurso não encontrado".
/// </summary>
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Executa o próximo passo do pipeline e, caso qualquer exceção escape
    /// dele, intercepta e delega o tratamento para <see cref="TratarExcecaoAsync"/>.
    /// </summary>
    /// <param name="context">Contexto da requisição HTTP atual.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await TratarExcecaoAsync(context, ex);
        }
    }

    /// <summary>
    /// Monta e escreve a resposta de erro no formato padrão da API,
    /// registrando também o ocorrido nos logs para facilitar investigação.
    /// </summary>
    /// <param name="context">Contexto da requisição HTTP atual.</param>
    /// <param name="exception">Exceção que foi lançada durante o processamento.</param>
    private async Task TratarExcecaoAsync(HttpContext context, Exception exception)
    {
        var (statusCode, titulo) = MapearExcecao(exception);

        _logger.LogError(exception,
            "Erro tratado: {Titulo} | Path: {Path}",
            titulo, context.Request.Path);

        var response = new ErroResponse
        {
            StatusCode = statusCode,
            Titulo = titulo,
            Mensagem = exception.Message,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Traduz o tipo da exceção lançada para o código HTTP e o título
    /// que devem ser retornados ao cliente. Esse é o único lugar que
    /// precisa ser alterado quando um novo tipo de erro de negócio
    /// precisar de um status HTTP específico.
    /// </summary>
    /// <param name="exception">Exceção que foi lançada durante o processamento.</param>
    /// <returns>O código HTTP e o título correspondente ao tipo da exceção.</returns>
    private static (int StatusCode, string Titulo) MapearExcecao(Exception exception)
    {
        return exception switch
        {
            KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Recurso não encontrado"),
            ArgumentException => ((int)HttpStatusCode.BadRequest, "Requisição inválida"),
            InvalidOperationException => ((int)HttpStatusCode.Conflict, "Operação inválida"),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Acesso não autorizado"),
            _ => ((int)HttpStatusCode.InternalServerError, "Erro interno do servidor")
        };
    }
}