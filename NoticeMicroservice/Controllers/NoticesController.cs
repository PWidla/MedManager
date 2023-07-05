using Domain.Entities;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace AppointmentMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #region notice
        [HttpGet]
        public ActionResult<IEnumerable<Notice>> GetNotices()
        {
            return _context.Notices;
        }

        [HttpGet("{noticeId:int}")]
        public async Task<ActionResult<Notice>> GetNoticeById(int noticeId)
        {
            var notice = await _context.Notices.FindAsync(noticeId);
            return notice;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNotice(Notice notice)
        {
            var noticeValidator = new NoticeValidator();
            var validationResult = await noticeValidator.ValidateAsync(notice);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateNotice(Notice notice)
        {
            var noticeValidator = new NoticeValidator();
            var validationResult = await noticeValidator.ValidateAsync(notice);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            _context.Notices.Update(notice);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{noticeId:int}")]
        public async Task<ActionResult> DeleteNotice(int noticeId)
        {
            var notice = await _context.Notices.FindAsync(noticeId);
            _context.Notices.Remove(notice);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}