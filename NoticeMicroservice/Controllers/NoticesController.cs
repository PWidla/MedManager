using NoticeMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace NoticeMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepository _noticeRepository;

        public NoticesController(INoticeRepository NoticeRepository)
        {
            _noticeRepository = NoticeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Notice>> GetNotices()
        {
            var Notices = _noticeRepository.GetNotices();
            return Ok(Notices);
        }

        [HttpGet("{noticeId:int}")]
        public ActionResult<Notice> GetNotice(int NoticeId)
        {
            var Notice = _noticeRepository.GetNotice(NoticeId);

            if (Notice == null)
                return NotFound();

            return Ok(Notice);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateNotice(Notice Notice)
        {
            var NoticeValidator = new NoticeValidator();
            ValidationResult validationResult = NoticeValidator.Validate(Notice);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var created = _noticeRepository.CreateNotice(Notice);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{noticeId:int}")]
        public ActionResult UpdateNotice(int noticeId, Notice notice)
        {
            if (noticeId != notice.Id)
                return BadRequest();

            var NoticeExists = _noticeRepository.NoticeExists(noticeId);

            if (!NoticeExists)
                return NotFound();

            var NoticeValidator = new NoticeValidator();
            ValidationResult validationResult = NoticeValidator.Validate(notice);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var updated = _noticeRepository.UpdateNotice(notice);

            if (!updated)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{noticeId:int}")]
        public ActionResult DeleteNotice(int noticeId)
        {
            var noticeExists = _noticeRepository.NoticeExists(noticeId);

            if (!noticeExists)
                return NotFound();

            var deleted = _noticeRepository.DeleteNotice(noticeId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}