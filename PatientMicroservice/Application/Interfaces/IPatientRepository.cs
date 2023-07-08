using Domain.Entities;

namespace PatientMicroservice.Application.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(int id);
        bool PatientExists(int id);
        bool CreatePatient(Patient Patient);
        bool UpdatePatient(Patient Patient);
        bool DeletePatient(int id);
        bool Save();
    }
}
