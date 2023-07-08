using DoctorMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorMicroservice.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{DoctorId:int}")]
        public ActionResult<Doctor> GetDoctor(int DoctorId)
        {
            var Doctor = _DoctorRepository.GetDoctor(DoctorId);

            if (Doctor == null)
                return NotFound();

            return Ok(Doctor);
        }

        [HttpPost]
        public ActionResult CreateDoctor(Doctor Doctor)
        {
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

            var created = _DoctorRepository.CreateDoctor(Doctor);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{DoctorId:int}")]
        public ActionResult UpdateDoctor(int DoctorId, Doctor Doctor)
        {
            if (DoctorId != Doctor.Id)
                return BadRequest();

            var DoctorExists = _DoctorRepository.DoctorExists(DoctorId);

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

        [HttpDelete("{DoctorId:int}")]
        public ActionResult DeleteDoctor(int DoctorId)
        {
            var DoctorExists = _DoctorRepository.DoctorExists(DoctorId);

            if (!DoctorExists)
                return NotFound();

            var deleted = _DoctorRepository.DeleteDoctor(DoctorId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}