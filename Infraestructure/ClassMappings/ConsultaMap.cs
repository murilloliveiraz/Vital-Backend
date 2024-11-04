using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.ClassMappings
{
    public class ConsultaMap : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("consultas")
      .HasKey(cst => cst.Id);

            builder.Property(ex => ex.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired();
            
            builder.Property(ex => ex.Status)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.ValorConsulta)
            .HasColumnType("VARCHAR");

            builder.Property(ex => ex.StatusPagamento)
            .HasColumnType("VARCHAR");
            
            builder.Property(ex => ex.PagamentoId)
            .HasColumnType("INTEGER");

            builder.Property(ex => ex.TipoConsulta)
            .HasColumnType("VARCHAR")
            .IsRequired();
            
            builder.Property(ex => ex.QueixasDoPaciente)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.Local)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.Data)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(ex => ex.MeetLink)
            .HasColumnType("VARCHAR");

            builder.HasOne(ex => ex.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(e => e.PacienteId);

            builder.HasOne(ex => ex.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(e => e.MedicoId);

            builder.Property(ex => ex.EmailParaReceberNotificacoes)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(ex => ex.PrefixoDaPasta)
            .HasColumnType("VARCHAR");
        }
    }
}
