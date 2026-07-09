using GastosResidenciais.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Data;

public class AppDbContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    // Essa configuração permite que toda entidade de transação seja excluída conforme a ação da entidade pai
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}