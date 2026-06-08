using OpteamixEmployeeManagementSystem.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.BusinessValidator
{
    public interface IProjectValidator
    {
        void Validate(CreateProjectDto dto);
        void Validate(UpdateProjectDto dto);
    }
}
