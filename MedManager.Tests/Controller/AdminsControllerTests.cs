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
            var admins = A.Fake<ICollection<Admin>>();
            var adminList = A.Fake<List<Admin>>();
            var controller = new AdminsController(_adminRepository);

            var result = controller.GetAdmins();

            //Assert.NotNull(result);
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
    }
}
