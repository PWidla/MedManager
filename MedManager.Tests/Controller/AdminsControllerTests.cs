using AdminMicroservice.Application.Interfaces;
using AdminMicroservice.Controllers;
using Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace MedManager.Tests.Controller
{
    public class AdminsControllerTests
    {
        private readonly IAdminRepository _adminRepository;

        public AdminsControllerTests()
        {
            _adminRepository = A.Fake<IAdminRepository>();
        }

        [Fact]
        public void AdminsController_GetAdmins_ReturnOK()
        {
            var controller = new AdminsController(_adminRepository);

            var result = controller.GetAdmins();

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<Admin>>>()
                .Which.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void AdminsController_CreateAdmin_ReturnsOK()
        {
            var adminCreate = A.Fake<Admin>();
            var admin = A.Fake<Admin>();
            var admins = A.Fake<ICollection<Admin>>();
            var adminList = A.Fake<List<Admin>>();
            A.CallTo(() => _adminRepository.GetAdminTrimToUpper(adminCreate)).Returns(admin);
            A.CallTo(() => _adminRepository.CreateAdmin(adminCreate)).Returns(true);
            var controller = new AdminsController(_adminRepository);

            var result = controller.CreateAdmin(adminCreate);

            result.Should().NotBeNull();
        }

        [Fact]
        public void AdminsController_UpdateAdmin_ReturnsBadRequest_WhenAdminIdDoesNotMatch()
        {
            var adminId = 1;
            var admin = A.Fake<Admin>();
            var controller = new AdminsController(_adminRepository);

            var result = controller.UpdateAdmin(adminId + 1, admin);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void AdminsController_DeleteAdmin_ReturnsOK()
        {
            var adminId = 1;
            A.CallTo(() => _adminRepository.AdminExists(adminId)).Returns(true);
            A.CallTo(() => _adminRepository.DeleteAdmin(adminId)).Returns(true);
            var controller = new AdminsController(_adminRepository);

            var result = controller.DeleteAdmin(adminId);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void AdminsController_DeleteAdmin_ReturnsNotFound_WhenAdminDoesNotExist()
        {
            var adminId = 1;
            A.CallTo(() => _adminRepository.AdminExists(adminId)).Returns(false);
            var controller = new AdminsController(_adminRepository);

            var result = controller.DeleteAdmin(adminId);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
