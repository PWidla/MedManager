using PatientMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PatientMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet("{patientId:int}")]
        public ActionResult<Patient> GetPatientById(int patientId)
        {
            var Patient = _PatientRepository.GetPatient(patientId);

            if (Patient == null)
                return NotFound();

            return Ok(Patient);
        }

        [HttpPost]
        public ActionResult CreatePatient(Patient Patient)
        {
            var PatientValidator = new PatientValidator();
            ValidationResult validationResult = PatientValidator.Validate(Patient);

            var patients = _PatientRepository.GetPatientTrimToUpper(Patient);

            if (patients != null)
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

            var created = _PatientRepository.CreatePatient(Patient);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{patientId:int}")]
        public ActionResult UpdatePatient(int patientId, Patient Patient)
        {
            if (patientId != Patient.Id)
                return BadRequest();

            var PatientExists = _PatientRepository.PatientExists(patientId);

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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{patientId:int}")]
        public ActionResult DeletePatient(int patientId)
        {
            var PatientExists = _PatientRepository.PatientExists(patientId);

            if (!PatientExists)
                return NotFound();

            var deleted = _PatientRepository.DeletePatient(patientId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}