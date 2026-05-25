using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class UsuarioDetalheMapping : IEntityTypeConfiguration<UsuarioDetalhe>
    {
        public void Configure(EntityTypeBuilder<UsuarioDetalhe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Sobrenome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Imagem)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Reputacao)
                .IsRequired();

            builder.Property(x => x.Experiencia)
                .IsRequired();

            builder.Property(x => x.Nivel)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.HasIndex(x => x.UsuarioId)
                .IsUnique();

            builder.HasIndex(x => x.Cidade);

            builder.HasIndex(x => x.Estado);

            builder.HasOne(x => x.Usuario)
                .WithOne(x => x.UsuarioDetalhes)
                .HasForeignKey<UsuarioDetalhe>(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("UsuarioDetalhes");
        }
    }
}