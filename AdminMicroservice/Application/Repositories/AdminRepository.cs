using AdminMicroservice.Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace AdminMicroservice.Application.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Admin> GetAdmins()
        {
            return _context.Admins.ToList();
        }

        public Admin GetAdmin(int id)
        {
            return _context.Admins.Find(id);
        }

        public bool AdminExists(int id)
        {
            return _context.Admins.Any(a => a.Id == id);
        }

        public bool CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            return Save();
        }

        public bool UpdateAdmin(Admin admin)
        {
            _context.Admins.Update(admin);
            return Save();
        }

        public bool DeleteAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null)
                return false;

            _context.Admins.Remove(admin);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
