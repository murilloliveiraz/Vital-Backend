using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.ClassMappings
{
    public class ProntuarioMap : IEntityTypeConfiguration<Prontuario>
    {
        public void Configure(EntityTypeBuilder<Prontuario> builder)
        {
            builder.ToTable("prontuarios")
            .HasKey(p => p.Id);

            builder.Property(p => p.DataDeCriacao)
            .HasColumnType("timestamp")
            .IsRequired();
        }
    }
}
