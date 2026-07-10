using GastosResidenciais.Dtos.Erro;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Controllers;

/// <summary>
/// Expõe as operações relacionadas ao cadastro e consulta de transações
/// financeiras. Regras de negócio (como a restrição de receita para menores
/// de idade) e erros como "não encontrado" são tratados pelo middleware
/// global de exceções, então os métodos aqui só se preocupam com o fluxo de sucesso.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransacaoController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacaoController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    /// <summary>
    /// Cadastra uma nova transação financeira para uma pessoa.
    /// </summary>
    /// <param name="request">Dados necessários para o cadastro da transação.</param>
    /// <returns>Os dados da transação recém-criada.</returns>
    /// <response code="201">Transação criada com sucesso.</response>
    /// <response code="400">Dados informados são inválidos.</response>
    /// <response code="404">A pessoa informada na transação não foi encontrada.</response>
    /// <response code="409"> A pessoa é menor de 18 anos e a transação não é uma despesa.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar([FromBody] CriarTransacaoRequest request)
    {
        var transacao = await _transacaoService.CriarAsync(request);

        return CreatedAtAction(nameof(ObterPorId), new { id = transacao.Id }, transacao);
    }

    /// <summary>
    /// Lista todas as transações financeiras cadastradas no sistema.
    /// </summary>
    /// <returns>Coleção com todas as transações cadastradas.</returns>
    /// <response code="200">Lista retornada com sucesso (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransacaoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarTodos()
    {
        var transacoes = await _transacaoService.ListarTodosAsync();

        return Ok(transacoes);
    }

    /// <summary>
    /// Retorna os dados de uma transação específica.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>Os dados da transação correspondente ao id informado.</returns>
    /// <response code="200">Transação encontrada com sucesso.</response>
    /// <response code="404">Nenhuma transação foi encontrada com o id informado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var transacao = await _transacaoService.ObterPorIdAsync(id);

        return Ok(transacao);
    }
}