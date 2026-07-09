using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GastosResidenciais.Models;

public class Pessoa
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Nome { get; set; }
    
    public int Idade { get; set; }
    
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    
    public Pessoa(){}

    [SetsRequiredMembers]
    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }
}