using Microsoft.EntityFrameworkCore;
using PAWPAL.Data;
using PAWPAL.Models;

namespace PAWPAL.Services
{
    public class AppointmentService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public AppointmentService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Appointment
                .Include(a => a.Pet)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Appointment.Include(a => a.Pet).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            using var context = _factory.CreateDbContext();
            context.Appointment.Add(appointment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            using var context = _factory.CreateDbContext();
            context.Appointment.Update(appointment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var appt = await context.Appointment.FindAsync(id);
            if (appt != null)
            {
                context.Appointment.Remove(appt);
                await context.SaveChangesAsync();
            }
        }
    }
}
