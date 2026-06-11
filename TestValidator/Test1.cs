using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpteamixEmployeeManagementSystem.Domain.BusinessValidators;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using System;

namespace TestValidator
{
    [TestClass]
    public sealed class ProjectValidatorTests
    {
        private ProjectValidator _validator = null!;

        [TestInitialize]
        public void Setup()
        {
            _validator = new ProjectValidator();
        }

        [TestMethod]
        public void CreateProject_ValidData_ShouldPass()
        {
            var dto = new CreateProjectDto
            {
                ProjectName = "Project A",
                ClientName = "Client A",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Budget = 10000,
                ManagerId = 1
            };

            _validator.Validate(dto);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CreateProject_EmptyProjectName_ShouldThrow()
        {
            var dto = new CreateProjectDto
            {
                ProjectName = "",
                ClientName = "Client A",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Budget = 10000,
                ManagerId = 1
            };

            Assert.Throws<Exception>(() => _validator.Validate(dto));
        }

        [TestMethod]
        public void CreateProject_InvalidBudget_ShouldThrow()
        {
            var dto = new CreateProjectDto
            {
                ProjectName = "Project A",
                ClientName = "Client A",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Budget = 0,
                ManagerId = 1
            };

            Assert.Throws<Exception>(() => _validator.Validate(dto));
        }


        [TestMethod]
        public void UpdateProject_ValidData_ShouldPass()
        {
            var dto = new UpdateProjectDto
            {
                ProjectId = 1,
                ProjectName = "Project A",
                ClientName = "Client A",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Budget = 10000,
                ManagerId = 1
            };

            _validator.Validate(dto);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateProject_InvalidProjectId_ShouldThrow()
        {
            var dto = new UpdateProjectDto
            {
                ProjectId = 0,
                ProjectName = "Project A",
                ClientName = "Client A",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                Budget = 10000,
                ManagerId = 1
            };

            Assert.Throws<Exception>(() => _validator.Validate(dto));
        }
    }
}