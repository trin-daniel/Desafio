using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Configuration;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");
        builder.HasKey(pessoa => pessoa.Id);
        builder.Property(pessoa => pessoa.Id);
        builder.Property(pessoa => pessoa.Nome);
        builder.Property(pessoa => pessoa.Idade);

        builder.HasMany(pessoa => pessoa.Transacoes).WithOne(transacao => transacao.Pessoa)
            .HasForeignKey(p => p.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}