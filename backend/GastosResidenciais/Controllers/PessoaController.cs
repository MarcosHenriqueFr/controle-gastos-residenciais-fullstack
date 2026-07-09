using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Controllers;

[ApiController]
[Route("api/pessoa")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPessoaRequest request)
    {
        var pessoa = await _pessoaService.CriarAsync(request);

        return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var pessoas = await _pessoaService.ObterTodasAsync();

        return Ok(pessoas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var pessoa = await _pessoaService.ObterPorIdAsync(id);

        return Ok(pessoa);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _pessoaService.RemoverAsync(id);

        return NoContent();
    }

    [HttpGet("{id:guid}/transacaos")]
    public async Task<IActionResult> ObterTransacoesPorPessoa(Guid id)
    {
        var transacoes = await _pessoaService.ObterTransacoesPorPessoaAsync(id);

        return Ok(transacoes);
    }
}