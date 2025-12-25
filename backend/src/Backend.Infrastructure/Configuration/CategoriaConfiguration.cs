using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Configuration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(categoria => categoria.Id);
        builder.Property(categoria => categoria.Id).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(categoria => categoria.Descricao).HasColumnType("nvarchar(84)").IsRequired();
        builder.Property(categoria => categoria.Finalidade).HasColumnType("int").HasConversion<int>();
    }
}