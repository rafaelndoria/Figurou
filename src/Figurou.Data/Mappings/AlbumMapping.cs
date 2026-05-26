using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class AlbumMapping : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Ano)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.ImagemCapa)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.TotalFigurinhas)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.Nome,
                x.Ano
            })
            .IsUnique();

            builder.HasMany(x => x.Paginas)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Figurinhas)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Selecoes)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Albuns");
        }
    }
}