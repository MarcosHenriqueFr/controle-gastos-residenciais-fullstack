using System.Net;
using System.Text.Json;
using GastosResidenciais.Dtos.Erro;

namespace GastosResidenciais.Exceptions;

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