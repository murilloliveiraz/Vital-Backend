using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.ClassMappings
{
    public class DocumentoMap : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("documentos")
      .HasKey(cst => cst.Id);

            builder.HasOne(doc => doc.Consulta)
                .WithMany(c => c.Documentos)
                .HasForeignKey(c => c.ConsultaId);

            builder.Property(ex => ex.S3KeyPath)
            .HasColumnType("VARCHAR");
        }
    }
}