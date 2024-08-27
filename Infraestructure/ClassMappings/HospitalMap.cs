using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.ClassMappings
{
    internal class HospitalMap
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.ToTable("hospitais")
           .HasKey(p => p.Id);

            builder.Property(h => h.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(h => h.Endereco)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(h => h.Telefone)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(h => h.DataInativacao)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.HasMany(h => h.Servicos)
                .WithOne(hs => hs.Hospital)
                .HasForeignKey(hs => hs.HospitalId);

            builder.HasMany(h => h.Medicos)
            .WithOne(m => m.Hospital)
            .HasForeignKey(m => m.HospitalId);
        }
    }
}
