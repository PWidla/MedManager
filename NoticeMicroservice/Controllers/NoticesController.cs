using NoticeMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NoticeMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepository _NoticeRepository;

        public NoticesController(INoticeRepository NoticeRepository)
        {
            _NoticeRepository = NoticeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Notice>> GetNotices()
        {
            var Notices = _NoticeRepository.GetNotices();
            return Ok(Notices);
        }

        [HttpGet("{noticeId:int}")]
        public ActionResult<Notice> GetNotice(int NoticeId)
        {
            var Notice = _NoticeRepository.GetNotice(NoticeId);

            if (Notice == null)
                return NotFound();

            return Ok(Notice);
        }

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

            var created = _NoticeRepository.CreateNotice(Notice);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{noticeId:int}")]
        public ActionResult UpdateNotice(int NoticeId, Notice Notice)
        {
            if (NoticeId != Notice.Id)
                return BadRequest();

            var NoticeExists = _NoticeRepository.NoticeExists(NoticeId);

            if (!NoticeExists)
                return NotFound();

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

            var updated = _NoticeRepository.UpdateNotice(Notice);

            if (!updated)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{noticeId:int}")]
        public ActionResult DeleteNotice(int NoticeId)
        {
            var NoticeExists = _NoticeRepository.NoticeExists(NoticeId);

            if (!NoticeExists)
                return NotFound();

            var deleted = _NoticeRepository.DeleteNotice(NoticeId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}