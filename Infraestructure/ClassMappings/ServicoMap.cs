using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.ClassMappings
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.ToTable("servicos")
           .HasKey(s => s.ServicoId);

            builder.Property(s => s.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(s => s.Especializacao)
            .HasColumnType("VARCHAR")
            .IsRequired();
            
            builder.Property(s => s.TipoServico)
            .HasColumnType("VARCHAR")
            .IsRequired();
            
            builder.Property(s => s.Valor)
            .HasColumnType("DECIMAL")
            .IsRequired();

            builder.Property(s => s.Descricao)
            .HasColumnType("VARCHAR");

            builder.HasMany(s => s.Hospitais)
                .WithOne(h => h.Servico)
                .HasForeignKey(h => h.ServicoId);

        }
    }
}
