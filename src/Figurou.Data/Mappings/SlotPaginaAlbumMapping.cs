using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class SlotPaginaAlbumMapping : IEntityTypeConfiguration<SlotPaginaAlbum>
    {
        public void Configure(EntityTypeBuilder<SlotPaginaAlbum> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.PosicaoX)
                .HasPrecision(10, 4)
                .IsRequired();

            builder.Property(x => x.PosicaoY)
                .HasPrecision(10, 4)
                .IsRequired();

            builder.Property(x => x.Largura)
                .HasPrecision(10, 4)
                .IsRequired();

            builder.Property(x => x.Altura)
                .HasPrecision(10, 4)
                .IsRequired();

            builder.Property(x => x.Ordem)
                .IsRequired();

            builder.HasIndex(x => x.PaginaAlbumId);

            builder.HasIndex(x => x.FigurinhaId)
                .IsUnique();

            builder.HasIndex(x => new
            {
                x.PaginaAlbumId,
                x.Ordem
            })
            .IsUnique();

            builder.HasOne(x => x.PaginaAlbum)
                .WithMany(x => x.SlotsPaginaAlbum)
                .HasForeignKey(x => x.PaginaAlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Figurinha)
                .WithMany(x => x.SlotsPaginaAlbum)
                .HasForeignKey(x => x.FigurinhaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("SlotsPaginaAlbum");
        }
    }
}