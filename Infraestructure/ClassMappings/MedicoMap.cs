using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.ClassMappings
{
    public class MedicoMap : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("medicos")
            .HasKey(m => m.Id);

            builder.Property(m => m.Especialidade)
            .HasColumnType("VARCHAR");

            builder.Property(m => m.CRM)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.HasOne(m => m.Usuario)
               .WithOne()
               .HasForeignKey<Medico>(m => m.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Hospital)
                .WithMany(h => h.Medicos)
                .HasForeignKey(m => m.HospitalId);
        }
    }
}
