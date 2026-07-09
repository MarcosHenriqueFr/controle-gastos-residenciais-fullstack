using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using GastosResidenciais.Enums;

namespace GastosResidenciais.Models;

public class Transacao
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Descricao { get; set; }
    public decimal Valor { get; set; }
    public TransacaoTipo Tipo { get; set; }

    public Guid PessoaId { get; set; }
    
    public Pessoa Pessoa { get; set; } = null!;
    
    public Transacao(){}

    [SetsRequiredMembers]
    public Transacao(string descricao, decimal valor, TransacaoTipo tipo, Guid pessoaId)
    {
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        PessoaId = pessoaId;
    }
}