using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Models;
using GastosResidenciais.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService service;

    public PessoaController(IPessoaService service)
    {
        this.service = service;
    }

    [HttpPost]
    public IActionResult CadastrarPessoa([FromBody] CriarPessoaRequest request)
    {
        var response = service.Cadastrar(request);
        return StatusCode(StatusCodes.Status201Created, response);
    }
}