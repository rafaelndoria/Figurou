using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Figurou.Data.Mapping
{
    public class MensagemConfiguration : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Conteudo)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Lida)
                .IsRequired();

            builder.Property(x => x.Editada)
                .IsRequired();

            builder.Property(x => x.Excluida)
                .IsRequired();

            builder.Property(x => x.DataEnvio)
                .IsRequired();

            builder.HasIndex(x => x.ConversaId);

            builder.HasIndex(x => x.UsuarioId);

            builder.HasIndex(x => x.DataEnvio);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Mensagens)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Conversa)
                .WithMany(x => x.Mensagens)
                .HasForeignKey(x => x.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Mensagens");
        }
    }
}