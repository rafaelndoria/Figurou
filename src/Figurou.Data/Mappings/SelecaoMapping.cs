using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class SelecaoMapping : IEntityTypeConfiguration<Selecao>
    {
        public void Configure(EntityTypeBuilder<Selecao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasIndex(x => x.AlbumId);

            builder.HasIndex(x => new
            {
                x.AlbumId,
                x.Nome
            })
            .IsUnique();

            builder.HasIndex(x => new
            {
                x.AlbumId,
                x.Codigo
            })
            .IsUnique();

            builder.HasOne(x => x.Album)
                .WithMany(x => x.Selecoes)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Selecoes");
        }
    }
}