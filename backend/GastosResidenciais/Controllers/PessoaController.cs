using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Dtos.Transacao;
using GastosResidenciais.Dtos.Erro;
using GastosResidenciais.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Controllers;

/// <summary>
/// Expõe as operações relacionadas ao cadastro de pessoas e à consulta
/// das transações financeiras de cada uma. Erros de negócio (como uma
/// pessoa não encontrada) são tratados pelo middleware global de exceções,
/// então os métodos aqui só se preocupam com o fluxo de sucesso.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    /// <param name="request">Dados necessários para o cadastro da pessoa.</param>
    /// <returns>Os dados da pessoa recém-criada.</returns>
    /// <response code="201">Pessoa criada com sucesso.</response>
    /// <response code="400">Dados informados são inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarPessoaRequest request)
    {
        var pessoa = await _pessoaService.CriarAsync(request);

        return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Coleção com todas as pessoas cadastradas no sistema.</returns>
    /// <response code="200">Lista retornada com sucesso (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PessoaResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodas()
    {
        var pessoas = await _pessoaService.ObterTodasAsync();

        return Ok(pessoas);
    }

    /// <summary>
    /// Retorna os dados de uma pessoa específica.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>Os dados da pessoa correspondente ao id informado.</returns>
    /// <response code="200">Pessoa encontrada com sucesso.</response>
    /// <response code="404">Nenhuma pessoa foi encontrada com o id informado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var pessoa = await _pessoaService.ObterPorIdAsync(id);

        return Ok(pessoa);
    }

    /// <summary>
    /// Remove uma pessoa do sistema.
    /// </summary>
    /// <param name="id">Identificador único da pessoa a ser removida.</param>
    /// <response code="204">Pessoa removida com sucesso (sem conteúdo no corpo da resposta).</response>
    /// <response code="404">Nenhuma pessoa foi encontrada com o id informado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _pessoaService.RemoverAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Lista todas as transações financeiras de uma pessoa específica.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>Coleção com as transações financeiras associadas à pessoa.</returns>
    /// <response code="200">Lista de transações retornada com sucesso (pode ser vazia).</response>
    /// <response code="404">Nenhuma pessoa foi encontrada com o id informado.</response>
    [HttpGet("{id:guid}/transacoes")]
    [ProducesResponseType(typeof(IEnumerable<TransacaoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterTransacoesPorPessoa(Guid id)
    {
        var transacoes = await _pessoaService.ObterTransacoesPorPessoaAsync(id);

        return Ok(transacoes);
    }
}