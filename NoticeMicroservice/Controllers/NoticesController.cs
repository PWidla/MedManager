using Domain.Entities;
using Domain.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace NoticeMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NoticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public NoticsController(ApplicationDbContext context)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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