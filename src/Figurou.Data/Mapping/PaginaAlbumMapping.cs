using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class PaginaAlbumMapping : IEntityTypeConfiguration<PaginaAlbum>
    {
        public void Configure(EntityTypeBuilder<PaginaAlbum> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NumeroPagina)
                .IsRequired();

            builder.Property(x => x.ImagemPagina)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Largura)
                .HasPrecision(10, 4)
                .IsRequired();

            builder.Property(x => x.Altura)
                .HasPrecision(10, 4)
                .IsRequired();

            builder.HasIndex(x => x.AlbumId);

            builder.HasIndex(x => new
            {
                x.AlbumId,
                x.NumeroPagina
            })
            .IsUnique();

            builder.HasOne(x => x.Album)
                .WithMany(x => x.Paginas)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PaginasAlbum");
        }
    }
}