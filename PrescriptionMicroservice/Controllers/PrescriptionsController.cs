using Domain.Entities;
using Domain.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using PrescriptionMicroservice.Application.Interfaces;

namespace PrescriptionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionsController(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prescription>> GetPrescriptions()
        {
            var prescriptions = _prescriptionRepository.GetPrescriptions();
            return Ok(prescriptions);
        }

        [HttpGet("{prescriptionId:int}")]
        public ActionResult<Prescription> GetPrescriptionById(int prescriptionId)
        {
            var prescription = _prescriptionRepository.GetPrescription(prescriptionId);

            if (prescription == null)
                return NotFound();

            return Ok(prescription);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Admin")]
        public async Task<ActionResult> CreatePrescription(Prescription prescription)
        {
            var prescriptionValidator = new PrescriptionValidator();
            var validationResult = await prescriptionValidator.ValidateAsync(prescription);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var created = _prescriptionRepository.CreatePrescription(prescription);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{prescriptionId:int}")]
        [Authorize(Roles = "Doctor, Admin")]
        public async Task<ActionResult> UpdatePrescription(int prescriptionId, Prescription prescription)
        {
            if (prescriptionId != prescription.Id)
                return BadRequest();

            var prescriptionExists = _prescriptionRepository.PrescriptionExists(prescriptionId);

            if (!prescriptionExists)
                return NotFound();

            var prescriptionValidator = new PrescriptionValidator();
            var validationResult = await prescriptionValidator.ValidateAsync(prescription);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var updated = _prescriptionRepository.UpdatePrescription(prescription);

            if (!updated)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{prescriptionId:int}")]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult DeletePrescription(int prescriptionId)
        {
            var prescriptionExists = _prescriptionRepository.PrescriptionExists(prescriptionId);

            if (!prescriptionExists)
                return NotFound();

            var deleted = _prescriptionRepository.DeletePrescription(prescriptionId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}