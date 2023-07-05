using Domain.Entities;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace AppointmentMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PrescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region prescription
        [HttpGet]
        public ActionResult<IEnumerable<Prescription>> GetPrescriptions()
        {
            return _context.Prescriptions;
        }

        [HttpGet("{prescriptionId:int}")]
        public async Task<ActionResult<Prescription>> GetPrescriptionById(int prescriptionId)
        {
            var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
            return prescription;
        }

        [HttpPost]
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
            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePrescription(Prescription prescription)
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
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{prescriptionId:int}")]
        public async Task<ActionResult> DeletePrescription(int prescriptionId)
        {
            var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}