using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.ClassMappings
{
    internal class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("pacientes")
           .HasKey(p => p.Id);

            builder.Property(p => p.DataNascimento)
            .HasColumnType("timestamp");

            builder.HasOne(m => m.Usuario)
               .WithOne()
               .HasForeignKey<Paciente>(m => m.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Exames)
                .WithOne(e => e.Paciente)
                .HasForeignKey(e => e.PacienteId);

            builder.HasOne(p => p.Prontuario)
               .WithOne()
               .HasForeignKey<Prontuario>(p => p.PacienteId);
        }
    }
}
