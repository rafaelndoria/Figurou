using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class TrocaMapping : IEntityTypeConfiguration<Troca>
    {
        public void Configure(EntityTypeBuilder<Troca> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(x => x.DataRequisicao)
                .IsRequired();

            builder.Property(x => x.DataFinalizacao)
                .IsRequired(false);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.SolicitanteId);

            builder.HasIndex(x => x.DestinatarioId);

            builder.HasOne(x => x.Solicitante)
                .WithMany(x => x.TrocasSolicitadas)
                .HasForeignKey(x => x.SolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Destinatario)
                .WithMany(x => x.TrocasRecebidas)
                .HasForeignKey(x => x.DestinatarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Trocas");
        }
    }
}