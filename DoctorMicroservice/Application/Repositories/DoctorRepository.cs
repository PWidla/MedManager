using DoctorMicroservice.Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace DoctorMicroservice.Application.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }

        public Doctor GetDoctor(int id)
        {
            return _context.Doctors.Find(id);
        }

        public bool DoctorExists(int id)
        {
            return _context.Doctors.Any(a => a.Id == id);
        }

        public bool CreateDoctor(Doctor Doctor)
        {
            _context.Doctors.Add(Doctor);
            return Save();
        }

        public bool UpdateDoctor(Doctor Doctor)
        {
            _context.Doctors.Update(Doctor);
            return Save();
        }

        public bool DeleteDoctor(int id)
        {
            var Doctor = _context.Doctors.Find(id);
            if (Doctor == null)
                return false;

            _context.Doctors.Remove(Doctor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
