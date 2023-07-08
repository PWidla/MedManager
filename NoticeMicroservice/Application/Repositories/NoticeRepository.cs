using Domain.Entities;
using NoticeMicroservice.Application.Interfaces;
using Persistence.Context;

namespace NoticeMicroservice.Application.Repositories
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly ApplicationDbContext _context;

        public NoticeRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public ICollection<Notice> GetNotices()
        {
            return _context.Notices.ToList();
        }

        public Notice GetNotice(int id)
        {
            return _context.Notices.FirstOrDefault(n => n.Id == id);
        }

        public bool NoticeExists(int id)
        {
            return _context.Notices.Any(n => n.Id == id);
        }

        public bool CreateNotice(Notice notice)
        {
            _context.Notices.Add(notice);
            return Save();
        }

        public bool UpdateNotice(Notice notice)
        {
            _context.Notices.Update(notice);
            return Save();
        }

        public bool DeleteNotice(int id)
        {
            var notice = _context.Notices.FirstOrDefault(n => n.Id == id);
            if (notice != null)
            {
                _context.Notices.Remove(notice);
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