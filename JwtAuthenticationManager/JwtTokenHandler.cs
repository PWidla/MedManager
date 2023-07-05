using Domain.Entities;
using Persistence.Context;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "kj2JMl2N44kjeL992N9zZ1lwedaNZ1aqL";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private List<Admin> _admins;
        private List<Doctor> _doctors;
        private List<Patient> _patients;

        private readonly ApplicationDbContext _context;
        public JwtTokenHandler(ApplicationDbContext context)
        {
            _context = context;
            _admins.AddRange(_context.Admins.ToList());
            _doctors.AddRange(_context.Doctors.ToList());
            _patients.AddRange(_context.Patients.ToList());
        }
    }
}
