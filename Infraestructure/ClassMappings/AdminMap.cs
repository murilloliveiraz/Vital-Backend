using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.ClassMappings
{
    public class AdminMap : IEntityTypeConfiguration<Administrador>
    {
        public void Configure(EntityTypeBuilder<Administrador> builder)
        {
            builder.ToTable("administradores")
      .HasKey(a => a.Id);

            builder.Property(a => a.Cargo)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.HasOne(a => a.Usuario)
              .WithOne()
              .HasForeignKey<Administrador>(a => a.UserId)
              .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
