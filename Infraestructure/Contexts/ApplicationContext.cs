using Domain;
using Infraestructure.ClassMappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts
{
    public class ApplicationContext : IdentityDbContext<Usuario>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Hospital> Hospitais { get; set; }
        public DbSet<HospitalServico> HospitalServicos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminMap());
            modelBuilder.ApplyConfiguration(new HospitalMap());
            modelBuilder.ApplyConfiguration(new HospitalServicoMap());
            modelBuilder.ApplyConfiguration(new ServicoMap());
            modelBuilder.ApplyConfiguration(new MedicoMap());
            modelBuilder.ApplyConfiguration(new ExameMap());
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new ConsultaMap());
            base.OnModelCreating(modelBuilder);
        }  
        public string ConnectionString()
        {
            return "Host=localhost;Port=5432;Database=hospital;Username=postgres;Password=123456;";

        }
    }
}
