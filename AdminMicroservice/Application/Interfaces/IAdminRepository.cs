using Domain.Entities;

namespace AdminMicroservice.Application.Interfaces
{
    public interface IAdminRepository
    {
        ICollection<Admin> GetAdmins();
        Admin GetAdmin(int id);
        bool AdminExists(int id);
        bool CreateAdmin(Admin admin);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(int id);
        bool Save();
    }
}
