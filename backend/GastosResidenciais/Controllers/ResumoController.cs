using GastosResidenciais.Dtos.Resumo;
using GastosResidenciais.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Controllers;

/// <summary>
/// Expõe o resumo financeiro consolidado do sistema: o total de receitas,
/// despesas e saldo de cada pessoa cadastrada, além do total geral somando
/// todas elas.
/// </summary>
[ApiController]
[Route("api/total")]
[Produces("application/json")]
public class ResumoController : ControllerBase
{
    private readonly IResumoService _resumoService;

    public ResumoController(IResumoService resumoService)
    {
        _resumoService = resumoService;
    }

    /// <summary>
    /// Retorna o resumo financeiro de todas as pessoas cadastradas,
    /// junto com o total geral do sistema.
    /// </summary>
    /// <returns>Resumo por pessoa e o total geral consolidado.</returns>
    /// <response code="200">Resumo calculado e retornado com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ResumoGeralResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Resumir()
    {
        var resumo = await _resumoService.Resumir();

        return Ok(resumo);
    }
}