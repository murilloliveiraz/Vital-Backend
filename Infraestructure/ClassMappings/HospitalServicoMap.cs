using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.ClassMappings
{
    public class HospitalServicoMap : IEntityTypeConfiguration<HospitalServico>
    {
        public void Configure(EntityTypeBuilder<HospitalServico> builder)
        {
            builder.ToTable("hospitalservico")
            .HasKey(hs => new { hs.HospitalId, hs.ServicoId });

            builder.HasOne(hs => hs.Servico)
                .WithMany(s => s.Hospitais)
                .HasForeignKey(hs => hs.ServicoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(hs => hs.Hospital)
                .WithMany(h => h.Servicos)
                .HasForeignKey(hs => hs.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
