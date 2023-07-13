using DoctorMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _DoctorRepository;

        public DoctorsController(IDoctorRepository DoctorRepository)
        {
            _DoctorRepository = DoctorRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            var Doctors = _DoctorRepository.GetDoctors();
            return Ok(Doctors);
        }

        [HttpGet("{doctorId:int}")]
        public ActionResult<Doctor> GetDoctorById(int doctorId)
        {
            var Doctor = _DoctorRepository.GetDoctor(doctorId);

            if (Doctor == null)
                return NotFound();

            return Ok(Doctor);
        }

        [HttpPost]
        public ActionResult CreateDoctor(Doctor Doctor)
        {
            var DoctorValidator = new DoctorValidator();
            ValidationResult validationResult = DoctorValidator.Validate(Doctor);

            var doctors = _DoctorRepository.GetDoctorTrimToUpper(Doctor);

            if (doctors != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var created = _DoctorRepository.CreateDoctor(Doctor);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{doctorId:int}")]
        public ActionResult UpdateDoctor(int doctorId, Doctor Doctor)
        {
            if (doctorId != Doctor.Id)
                return BadRequest();

            var DoctorExists = _DoctorRepository.DoctorExists(doctorId);

            if (!DoctorExists)
                return NotFound();

            var DoctorValidator = new DoctorValidator();
            ValidationResult validationResult = DoctorValidator.Validate(Doctor);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var updated = _DoctorRepository.UpdateDoctor(Doctor);

            if (!updated)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{doctorId:int}")]
        public ActionResult DeleteDoctor(int doctorId)
        {
            var DoctorExists = _DoctorRepository.DoctorExists(doctorId);

            if (!DoctorExists)
                return NotFound();

            var deleted = _DoctorRepository.DeleteDoctor(doctorId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}