using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class FigurinhaMapping : IEntityTypeConfiguration<Figurinha>
    {
        public void Configure(EntityTypeBuilder<Figurinha> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.Numero)
                .IsRequired();

            builder.Property(x => x.Raridade)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.NomeJogador)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.HasOne(x => x.Album)
                .WithMany(x => x.Figurinhas)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PaginaAlbum)
                .WithMany(x => x.Figurinhas)
                .HasForeignKey(x => x.PaginaAlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.AlbumId,
                x.PaginaAlbumId,
                x.Codigo
            })
            .IsUnique();

            builder.HasIndex(x => new
            {
                x.AlbumId,
                x.PaginaAlbumId,
                x.Numero
            })
            .IsUnique();

            builder.ToTable("Figurinhas");
        }
    }
}