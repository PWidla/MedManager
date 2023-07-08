using PatientMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace PatientMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _PatientRepository;

        public PatientsController(IPatientRepository PatientRepository)
        {
            _PatientRepository = PatientRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            var Patients = _PatientRepository.GetPatients();
            return Ok(Patients);
        }

        [HttpGet("{PatientId:int}")]
        public ActionResult<Patient> GetPatient(int PatientId)
        {
            var Patient = _PatientRepository.GetPatient(PatientId);

            if (Patient == null)
                return NotFound();

            return Ok(Patient);
        }

        [HttpPost]
        public ActionResult CreatePatient(Patient Patient)
        {
            var PatientValidator = new PatientValidator();
            ValidationResult validationResult = PatientValidator.Validate(Patient);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var created = _PatientRepository.CreatePatient(Patient);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{PatientId:int}")]
        public ActionResult UpdatePatient(int PatientId, Patient Patient)
        {
            if (PatientId != Patient.Id)
                return BadRequest();

            var PatientExists = _PatientRepository.PatientExists(PatientId);

            if (!PatientExists)
                return NotFound();

            var PatientValidator = new PatientValidator();
            ValidationResult validationResult = PatientValidator.Validate(Patient);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var updated = _PatientRepository.UpdatePatient(Patient);

            if (!updated)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{PatientId:int}")]
        public ActionResult DeletePatient(int PatientId)
        {
            var PatientExists = _PatientRepository.PatientExists(PatientId);

            if (!PatientExists)
                return NotFound();

            var deleted = _PatientRepository.DeletePatient(PatientId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}