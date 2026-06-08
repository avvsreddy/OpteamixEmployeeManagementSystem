using System;
using System.Collections.Generic;
using System.Text;

namespace OpteamixEmployeeManagementSystem.Domain.DTOs
{
    public class RefreshTokenDto
    {
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
