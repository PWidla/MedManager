using Domain.Entities;
using PatientMicroservice.Application.Interfaces;
using Persistence.Context;

namespace PatientMicroservice.Application.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public ICollection<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatient(int id)
        {
            return _context.Patients.FirstOrDefault(n => n.Id == id);
        }

        public bool PatientExists(int id)
        {
            return _context.Patients.Any(n => n.Id == id);
        }

        public bool CreatePatient(Patient Patient)
        {
            _context.Patients.Add(Patient);
            return Save();
        }

        public bool UpdatePatient(Patient Patient)
        {
            _context.Patients.Update(Patient);
            return Save();
        }

        public bool DeletePatient(int id)
        {
            var Patient = _context.Patients.FirstOrDefault(n => n.Id == id);
            if (Patient != null)
            {
                _context.Patients.Remove(Patient);
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