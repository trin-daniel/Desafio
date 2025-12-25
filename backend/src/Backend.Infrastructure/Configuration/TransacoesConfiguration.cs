using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Configuration;

public class TransacoesConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes");
        builder.HasKey(transacao => transacao.Id);
        builder.Property(transacao => transacao.Id);
        builder.Property(transacao => transacao.Valor).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(transacao => transacao.Descricao).IsRequired().HasColumnType("varchar").HasMaxLength(64);
        builder.Property(transacao => transacao.Tipo).IsRequired().HasColumnType("int").HasConversion<int>();

        // por que delete restrict?
        builder.HasOne(transacao => transacao.Categoria).WithMany(categoria => categoria.Transacaos)
            .HasForeignKey(transacao => transacao.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(transacao => transacao.Pessoa).WithMany(pessoa => pessoa.Transacoes)
            .HasForeignKey(transacao => transacao.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}