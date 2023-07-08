using Domain.Entities;

namespace DoctorMicroservice.Application.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(int id);
        bool DoctorExists(int id);
        bool CreateDoctor(Doctor Doctor);
        bool UpdateDoctor(Doctor Doctor);
        bool DeleteDoctor(int id);
        bool Save();
    }
}
