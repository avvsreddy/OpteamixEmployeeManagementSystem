using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpteamixEmployeeManagementSystem.API.Controllers;
using OpteamixEmployeeManagementSystem.API.Services;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;


namespace UserManagement.Tests
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private Mock<TokenServices> _tokenServiceMock;
        private Mock<EmailService> _emailServiceMock;
        private AuthController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Setup UserManager mock
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Setup RoleManager mock
            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                roleStoreMock.Object, null, null, null, null);

            // Setup TokenServices mock
            var configMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _tokenServiceMock = new Mock<TokenServices>(configMock.Object);

            // Setup EmailService mock
            // Setup EmailService mock
            var emailSettingsMock = new Mock<Microsoft.Extensions.Options.IOptions<OpteamixEmployeeManagementSystem.Domain.Settings.EmailSettings>>();
            _emailServiceMock = new Mock<EmailService>(emailSettingsMock.Object);

            // Create controller
            _controller = new AuthController(
                _userManagerMock.Object,
                _roleManagerMock.Object,
                _tokenServiceMock.Object,
                _emailServiceMock.Object);
        }

        // Test 1 - Login with wrong email
        [TestMethod]
        public async Task Login_WrongEmail_ReturnsUnauthorized()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var dto = new LoginDto
            {
                Email = "wrong@gmail.com",
                Password = "Wrong@1234"
            };

            // Act
            var result = await _controller.Login(dto);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        // Test 2 - Login with wrong password
        [TestMethod]
        public async Task Login_WrongPassword_ReturnsUnauthorized()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "john@gmail.com",
                FullName = "John"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>()))
                .ReturnsAsync(false);

            var dto = new LoginDto
            {
                Email = "john@gmail.com",
                Password = "WrongPass@1234"
            };

            // Act
            var result = await _controller.Login(dto);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        // Test 3 - Login with correct credentials
        [TestMethod]
        public async Task Login_CorrectCredentials_ReturnsToken()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Id = "123",
                Email = "john@gmail.com",
                FullName = "John"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>()))
                .ReturnsAsync(true);

            _userManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Employee" });

            _userManagerMock.Setup(x => x.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            _tokenServiceMock.Setup(x => x.GenerateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns("fake-jwt-token");

            _tokenServiceMock.Setup(x => x.GenerateRefreshToken())
                .Returns("fake-refresh-token");

            var dto = new LoginDto
            {
                Email = "john@gmail.com",
                Password = "John@1234"
            };

            // Act
            var result = await _controller.Login(dto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        // Test 4 - Register with valid data
        [TestMethod]
        public async Task Register_ValidData_ReturnsOk()
        {
            // Arrange
            _userManagerMock.Setup(x => x.CreateAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _roleManagerMock.Setup(x => x.RoleExistsAsync("Employee"))
                .ReturnsAsync(true);

            _userManagerMock.Setup(x => x.AddToRoleAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var dto = new RegisterDto
            {
                FullName = "Test User",
                Email = "test@gmail.com",
                Password = "Test@1234"
            };

            // Act
            var result = await _controller.Register(dto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        // Test 5 - Register with invalid data
        [TestMethod]
        public async Task Register_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            _userManagerMock.Setup(x => x.CreateAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(
                    new IdentityError { Description = "Password too weak" }));

            var dto = new RegisterDto
            {
                FullName = "Test User",
                Email = "test@gmail.com",
                Password = "weak"
            };

            // Act
            var result = await _controller.Register(dto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}