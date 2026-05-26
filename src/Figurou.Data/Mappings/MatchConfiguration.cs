using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Compatibilidade)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.UsuarioOrigemId,
                x.UsuarioDestinoId
            })
            .IsUnique();

            builder.HasOne(x => x.UsuarioOrigem)
                .WithMany(x => x.MatchesSolicitados)
                .HasForeignKey(x => x.UsuarioOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UsuarioDestino)
                .WithMany(x => x.MatchesRecebidos)
                .HasForeignKey(x => x.UsuarioDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Matches", tb =>
            {
                tb.HasCheckConstraint(
                    "CK_Match_Compatibilidade",
                    "[Compatibilidade] >= 0 AND [Compatibilidade] <= 100");
            });
        }
    }
}