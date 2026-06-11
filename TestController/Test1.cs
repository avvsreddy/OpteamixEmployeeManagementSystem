using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpteamixEmployeeManagementSystem.API.Controllers;
using OpteamixEmployeeManagementSystem.Domain.BusinessValidators;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace TestController
{
    [TestClass]
    public sealed class ProjectControllerTests
    {
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IProjectValidator> _projectValidatorMock;
        private ProjectController _controller;

        [TestInitialize]
        public void Setup()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _projectValidatorMock = new Mock<IProjectValidator>();

            _controller = new ProjectController(
                _projectRepositoryMock.Object,
                _projectValidatorMock.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project
                {
                    ProjectId = 1,
                    ProjectName = "Project A",
                    ClientName = "Client A",
                    Budget = 10000,
                    ManagerId = 1
                },
                new Project
                {
                    ProjectId = 2,
                    ProjectName = "Project B",
                    ClientName = "Client B",
                    Budget = 20000,
                    ManagerId = 2
                }
            };

            _projectRepositoryMock
                .Setup(x => x.GetAllProjectsAsync())
                .ReturnsAsync(projects);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);

            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetById_WhenProjectExists_ReturnsOkResult()
        {
            // Arrange
            var project = new Project
            {
                ProjectId = 1,
                ProjectName = "Project A",
                ClientName = "Client A",
                Budget = 10000,
                ManagerId = 1
            };

            _projectRepositoryMock
                .Setup(x => x.GetProjectByIdAsync(1))
                .ReturnsAsync(project);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetById_WhenProjectDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _projectRepositoryMock
                .Setup(x => x.GetProjectByIdAsync(1))
                .ReturnsAsync((Project?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task GetViewProjectSummary_ReturnsOkResult()
        {
            // Arrange
            var summary = new ViewProjectSummaryDto
            {
                TotalProjects = 10,
                ActiveProjects = 5,
                InactiveProjects = 2,
                CompletedProjects = 3,
                TotalBudget = 100000
            };

            _projectRepositoryMock
                .Setup(x => x.GetViewProjectSummaryAsync())
                .ReturnsAsync(summary);

            // Act
            var result = await _controller.GetViewProjectSummary();

            // Assert
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_WhenProjectExists_ReturnsNoContent()
        {
            // Arrange
            _projectRepositoryMock
                .Setup(x => x.DeleteProjectAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_WhenProjectDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _projectRepositoryMock
                .Setup(x => x.DeleteProjectAsync(1))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}