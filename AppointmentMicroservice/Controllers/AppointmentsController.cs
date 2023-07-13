using AppointmentMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace AppointmentMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsController(ApplicationDbContext context, IAppointmentRepository appointmentRepository)
        {
            _context = context;
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            var appointments = _appointmentRepository.GetAppointments().ToList();
            return Ok(appointments);
        }

        [HttpGet("{appointmentId:int}")]
        [Authorize]
        public ActionResult<Appointment> GetAppointmentById(int appointmentId)
        {
            var appointment = _appointmentRepository.GetAppointment(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult CreateAppointment(Appointment appointment)
        {
            var appointmentValidator = new AppointmentValidator();
            var validationResult = appointmentValidator.Validate(appointment);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            bool created = _appointmentRepository.CreateAppointment(appointment);
            if (!created)
            {
                return StatusCode(500, "Failed to create appointment.");
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult UpdateAppointment(Appointment appointment)
        {
            var appointmentValidator = new AppointmentValidator();
            var validationResult = appointmentValidator.Validate(appointment);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            bool updated = _appointmentRepository.UpdateAppointment(appointment);
            if (!updated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{appointmentId:int}")]
        [Authorize(Roles = "Doctor, Admin")]
        public ActionResult DeleteAppointment(int appointmentId)
        {
            bool deleted = _appointmentRepository.DeleteAppointment(appointmentId);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}