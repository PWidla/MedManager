using Domain.Entities;

namespace AppointmentMicroservice.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        ICollection<Appointment> GetAppointments();
        Appointment GetAppointment(int id);
        bool AppointmentExists(int id);
        bool CreateAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
        bool DeleteAppointment(int id);
        bool Save();
    }
}
