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
//#region admin
//[HttpGet]
//public ActionResult<IEnumerable<Admin>> GetAdmins()
//{
//    return _context.Admins;
//}

//[HttpGet("{adminId:int}")]
//public async Task<ActionResult<Admin>> GetAdminById(int adminId)
//{
//    var admin = await _context.Admins.FindAsync(adminId);
//    return admin;
//}

//[HttpPost]
//public async Task<ActionResult> CreateAdmin(Admin admin)
//{
//    var adminValidator = new AdminValidator();
//    var validationResult = await adminValidator.ValidateAsync(admin);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }

//    await _context.Admins.AddAsync(admin);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpPut]
//public async Task<ActionResult> UpdateAdmin(Admin admin)
//{
//    var adminValidator = new AdminValidator();
//    var validationResult = await adminValidator.ValidateAsync(admin);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    _context.Admins.Update(admin);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpDelete("{adminId:int}")]
//public async Task<ActionResult> DeleteAdmin(int adminId)
//{
//    var admin = await _context.Admins.FindAsync(adminId);
//    _context.Admins.Remove(admin);
//    await _context.SaveChangesAsync();
//    return Ok();
//}
//#endregion admin

//#region doctor
//[HttpGet]
//public ActionResult<IEnumerable<Doctor>> GetDoctors()
//{
//    return _context.Doctors;
//}

//[HttpGet("{doctorId:int}")]
//public async Task<ActionResult<Doctor>> GeDoctorById(int doctorId)
//{
//    var doctor = await _context.Doctors.FindAsync(doctorId);
//    return doctor;
//}

//[HttpPost]
//public async Task<ActionResult> CreateDoctor(Doctor doctor)
//{
//    var doctorValidator = new DoctorValidator();
//    var validationResult = await doctorValidator.ValidateAsync(doctor);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    await _context.Doctors.AddAsync(doctor);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpPut]
//public async Task<ActionResult> UpdateDoctor(Doctor doctor)
//{
//    var doctorValidator = new DoctorValidator();
//    var validationResult = await doctorValidator.ValidateAsync(doctor);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    _context.Doctors.Update(doctor);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpDelete("{doctorId:int}")]
//public async Task<ActionResult> DeleteDoctor(int doctorId)
//{
//    var doctor = await _context.Doctors.FindAsync(doctorId);
//    _context.Doctors.Remove(doctor);
//    await _context.SaveChangesAsync();
//    return Ok();
//}
//#endregion

//#region notice
//[HttpGet]
//public ActionResult<IEnumerable<Notice>> GetNotices()
//{
//    return _context.Notices;
//}

//[HttpGet("{noticeId:int}")]
//public async Task<ActionResult<Notice>> GetNoticeById(int noticeId)
//{
//    var notice = await _context.Notices.FindAsync(noticeId);
//    return notice;
//}

//[HttpPost]
//public async Task<ActionResult> CreateNotice(Notice notice)
//{
//    var noticeValidator = new NoticeValidator();
//    var validationResult = await noticeValidator.ValidateAsync(notice);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    await _context.Notices.AddAsync(notice);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpPut]
//public async Task<ActionResult> UpdateNotice(Notice notice)
//{
//    var noticeValidator = new NoticeValidator();
//    var validationResult = await noticeValidator.ValidateAsync(notice);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    _context.Notices.Update(notice);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpDelete("{noticeId:int}")]
//public async Task<ActionResult> DeleteNotice(int noticeId)
//{
//    var notice = await _context.Notices.FindAsync(noticeId);
//    _context.Notices.Remove(notice);
//    await _context.SaveChangesAsync();
//    return Ok();
//}
//#endregion

//#region patient
//[HttpGet]
//public ActionResult<IEnumerable<Patient>> GetPatients()
//{
//    return _context.Patients;
//}

//[HttpGet("{patientId:int}")]
//public async Task<ActionResult<Patient>> GetPatientById(int patientId)
//{
//    var patient = await _context.Patients.FindAsync(patientId);
//    return patient;
//}

//[HttpPost]
//public async Task<ActionResult> CreatePatient(Patient patient)
//{
//    var patientValidator = new PatientValidator();
//    var validationResult = await patientValidator.ValidateAsync(patient);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    await _context.Patients.AddAsync(patient);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpPut]
//public async Task<ActionResult> UpdatePatient(Patient patient)
//{
//    var patientValidator = new PatientValidator();
//    var validationResult = await patientValidator.ValidateAsync(patient);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    _context.Patients.Update(patient);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpDelete("{patientId:int}")]
//public async Task<ActionResult> DeletePatient(int patientId)
//{
//    var patient = await _context.Patients.FindAsync(patientId);
//    _context.Patients.Remove(patient);
//    await _context.SaveChangesAsync();
//    return Ok();
//}
//#endregion

//#region prescription
//[HttpGet]
//public ActionResult<IEnumerable<Prescription>> GetPrescriptions()
//{
//    return _context.Prescriptions;
//}

//[HttpGet("{prescriptionId:int}")]
//public async Task<ActionResult<Prescription>> GetPrescriptionById(int prescriptionId)
//{
//    var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
//    return prescription;
//}

//[HttpPost]
//public async Task<ActionResult> CreatePrescription(Prescription prescription)
//{
//    var prescriptionValidator = new PrescriptionValidator();
//    var validationResult = await prescriptionValidator.ValidateAsync(prescription);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    await _context.Prescriptions.AddAsync(prescription);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpPut]
//public async Task<ActionResult> UpdatePrescription(Prescription prescription)
//{
//    var prescriptionValidator = new PrescriptionValidator();
//    var validationResult = await prescriptionValidator.ValidateAsync(prescription);

//    if (!validationResult.IsValid)
//    {
//        foreach (var error in validationResult.Errors)
//        {
//            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
//        }
//        return BadRequest(ModelState);
//    }
//    _context.Prescriptions.Update(prescription);
//    await _context.SaveChangesAsync();
//    return Ok();
//}

//[HttpDelete("{prescriptionId:int}")]
//public async Task<ActionResult> DeletePrescription(int prescriptionId)
//{
//    var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
//    _context.Prescriptions.Remove(prescription);
//    await _context.SaveChangesAsync();
//    return Ok();
//}
//#endregion
