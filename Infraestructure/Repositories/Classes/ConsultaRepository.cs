using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationContext _context;

        public ConsultaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Consulta> Create(Consulta model)
        {
            model.Status = "Agendado";
            await _context.Consultas.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Consulta model)
        {
            model.Status = "Cancelado";
            await Update(model);
        }

        public async Task<Consulta> SetAppointmentAsCompleted(int id)
        {
            Consulta consultaAtDatabase = await _context.Consultas.FirstOrDefaultAsync(e => e.Id == id);

            consultaAtDatabase.Status = "Concluido";

            await _context.SaveChangesAsync();
            return consultaAtDatabase;
        }

        public async Task<IEnumerable<Consulta?>> Get()
        {
            return await _context.Consultas.AsNoTracking()
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>?> GetAllCompleted()
        {
            return await _context.Consultas.AsNoTracking().Where(e => e.Status == "Concluido")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>?> GetAllDoctorAppointmentsCompleted(int id)
        {
            return await _context.Consultas.AsNoTracking().Where(e => e.MedicoId == id && e.Status == "Concluido")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>?> GetAllDoctorAppointmentsScheduled(int id)
        {
            return await _context.Consultas.AsNoTracking().Where(e => e.MedicoId == id && e.Status == "Agendado")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>?> GetAllPatientAppointmentsCompleted(int id)
        {
            return await _context.Consultas.AsNoTracking().Where(e => e.PacienteId == id && e.Status == "Concluido")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>?> GetAllPatientAppointmentsScheduled(int id)
        {
            return await _context.Consultas.AsNoTracking().Where(e => e.PacienteId == id && e.Status == "Agendado")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>?> GetAllScheduled()
        {
            return await _context.Consultas.AsNoTracking().Where(e => e.Status == "Agendado")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<Consulta?> GetById(int id)
        {
            return await _context.Consultas.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Consulta> Update(Consulta model)
        {
            Consulta appointmentAtDatabase = await _context.Consultas.FirstOrDefaultAsync(e => e.Id == model.Id);

            _context.Entry(appointmentAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return appointmentAtDatabase;
        }
    }
}
