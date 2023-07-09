using AdminMicroservice.Application.Interfaces;
using Domain.Entities;
using Domain.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace AdminMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminsController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Admin>> GetAdmins()
        {
            var admins = _adminRepository.GetAdmins();
            return Ok(admins);
        }

        [HttpGet("{adminId:int}")]
        public ActionResult<Admin> GetAdminById(int adminId)
        {
            var admin = _adminRepository.GetAdmin(adminId);

            if (admin == null)
                return NotFound();

            return Ok(admin);
        }

        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            var adminValidator = new AdminValidator();
            ValidationResult validationResult = adminValidator.Validate(admin);

            var users = _adminRepository.GetAdminTrimToUpper(admin);

            if (users != null)
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

            var created = _adminRepository.CreateAdmin(admin);

            if (!created)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{adminId:int}")]
        public ActionResult UpdateAdmin(int adminId, Admin admin)
        {
            if (adminId != admin.Id)
                return BadRequest();

            var adminExists = _adminRepository.AdminExists(adminId);

            if (!adminExists)
                return NotFound();

            var adminValidator = new AdminValidator();
            ValidationResult validationResult = adminValidator.Validate(admin);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var updated = _adminRepository.UpdateAdmin(admin);

            if (!updated)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{adminId:int}")]
        public ActionResult DeleteAdmin(int adminId)
        {
            var adminExists = _adminRepository.AdminExists(adminId);

            if (!adminExists)
                return NotFound();

            var deleted = _adminRepository.DeleteAdmin(adminId);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}