using AppointmentMicroservice.Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace AppointmentMicroservice.Application.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Appointment> GetAppointments()
        {
            return _context.Appointments.ToList();
        }

        public Appointment GetAppointment(int id)
        {
            return _context.Appointments.FirstOrDefault(a => a.Id == id);
        }

        public bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(a => a.Id == id);
        }

        public bool CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            return Save();
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return Save();
        }

        public bool DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}