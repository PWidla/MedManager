using Domain.Entities;

namespace AppointmentMicroservice.Application.Interfaces
{
    public interface INoticeRepository
    {
        ICollection<Notice> GetNotices();
        Notice GetNotice(int id);
        bool NoticeExists(int id);
        bool CreateNotice(Notice notice);
        bool UpdateNotice(Notice notice);
        bool DeleteNotice(int id);
        bool Save();
    }
}
