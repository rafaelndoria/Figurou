using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class TrocaItemMapping : IEntityTypeConfiguration<TrocaItem>
    {
        public void Configure(EntityTypeBuilder<TrocaItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.HasIndex(x => x.TrocaId);

            builder.HasIndex(x => x.UsuarioId);

            builder.HasIndex(x => x.FigurinhaId);

            builder.HasIndex(x => new
            {
                x.TrocaId,
                x.UsuarioId,
                x.FigurinhaId
            })
            .IsUnique();

            builder.HasOne(x => x.Troca)
                .WithMany(x => x.TrocaItens)
                .HasForeignKey(x => x.TrocaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Figurinha)
                .WithMany(x => x.TrocaItens)
                .HasForeignKey(x => x.FigurinhaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.TrocaItens)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("TrocaItens");
        }
    }
}