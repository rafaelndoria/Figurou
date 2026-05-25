using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class FigurinhaUsuarioMapping : IEntityTypeConfiguration<FigurinhaUsuario>
    {
        public void Configure(EntityTypeBuilder<FigurinhaUsuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PossuiNoAlbum)
                .IsRequired();

            builder.Property(x => x.QuantidadeRepetida)
                .IsRequired();

            builder.Property(x => x.DisponivelTroca)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.FigurinhasUsuario)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.Figurinha)
                .WithMany(x => x.FigurinhasUsuario)
                .HasForeignKey(x => x.FigurinhaId);

            builder.ToTable("FigurinhasUsuario");
        }
    }
}
