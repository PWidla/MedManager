using Domain.Entities;

namespace PrescriptionMicroservice.Application.Interfaces
{
    public interface IPrescriptionRepository
    {
        ICollection<Prescription> GetPrescriptions();
        Prescription GetPrescription(int id);
        bool PrescriptionExists(int id);
        bool CreatePrescription(Prescription prescription);
        bool UpdatePrescription(Prescription prescription);
        bool DeletePrescription(int id);
        bool Save();
    }
}
