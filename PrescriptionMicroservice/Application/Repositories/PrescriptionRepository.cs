using Domain.Entities;
using Persistence.Context;
using PrescriptionMicroservice.Application.Interfaces;

namespace PrescriptionMicroservice.Application.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Prescription> GetPrescriptions()
        {
            return _context.Prescriptions.ToList();
        }

        public Prescription GetPrescription(int id)
        {
            return _context.Prescriptions.Find(id);
        }

        public bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(p => p.Id == id);
        }

        public bool CreatePrescription(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            return Save();
        }

        public bool UpdatePrescription(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            return Save();
        }

        public bool DeletePrescription(int id)
        {
            var prescription = _context.Prescriptions.Find(id);
            if (prescription == null)
                return false;

            _context.Prescriptions.Remove(prescription);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
