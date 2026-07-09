using GastosResidenciais.Dtos.Pessoa;
using GastosResidenciais.Models;

namespace GastosResidenciais.Mappers;

public static class PessoaMapper
{
    public static Pessoa ToEntity(CriarPessoaRequest request)
    {
        return new Pessoa(request.Nome, request.Idade);
    }

    public static PessoaResponse ToDto(Pessoa pessoa)
    {
        return new PessoaResponse(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }
}