using GastosResidenciais.Dtos.Resumo;

namespace GastosResidenciais.Services;

public interface IResumoService
{
    ResumoResponse Resumir();
}