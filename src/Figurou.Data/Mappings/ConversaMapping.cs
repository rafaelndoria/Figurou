using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class ConversaMapping : IEntityTypeConfiguration<Conversa>
    {
        public void Configure(EntityTypeBuilder<Conversa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataCriacao)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasIndex(x => x.TrocaId)
                .IsUnique();

            builder.HasOne(x => x.Troca)
                .WithOne()
                .HasForeignKey<Conversa>(x => x.TrocaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Conversas");
        }
    }
}