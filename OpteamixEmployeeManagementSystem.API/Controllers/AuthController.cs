using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpteamixEmployeeManagementSystem.API.Services;
using OpteamixEmployeeManagementSystem.Domain.DTOs;
using OpteamixEmployeeManagementSystem.Domain.Entities;
using System.Security.Claims;


namespace OpteamixEmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //dependency Injection
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenServices _tokenService;
        private readonly EmailService _emailService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TokenServices tokenService, EmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            //var user = new ApplicationUser
            //{
            //    FullName = dto.FullName,
            //    Email = dto.Email,
            //    UserName = dto.Email,
            //    CreatedDate = DateTime.UtcNow
            //};
            var count = _userManager.Users.Count();
            var employeeCode = $"EMP{(count + 1).ToString("D3")}";

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                CreatedDate = DateTime.UtcNow,
                EmployeeCode = employeeCode
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync("Employee"))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole("Employee"));
            }

            await _userManager.AddToRoleAsync(
                user,
                "Employee");

            return Ok(new
            {
                UserId = user.Id,
                EmployeeCode = user.EmployeeCode
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(
                dto.Email);

            if (user == null)
                return Unauthorized();

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!validPassword)
                return Unauthorized();

            var roles =
                await _userManager.GetRolesAsync(user);

            var token =
                _tokenService.GenerateToken(
                    user.Id,
                    user.Email!,
                    roles.FirstOrDefault() ?? "Employee");


            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }


        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));

            //var existingRoles = await _userManager.GetRolesAsync(user);
            //await _userManager.RemoveFromRolesAsync(user, existingRoles);

            //var result = await _userManager.AddToRoleAsync(user, dto.Role);

            // Check if role already assigned
            if (await _userManager.IsInRoleAsync(user, dto.Role))
                return BadRequest(new { Message = $"User already has {dto.Role} role" });

            var result = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = $"Role {dto.Role} assigned to {dto.Email}" });
        }


        //[HttpGet("profile")]
        //[Authorize]
        //public async Task<IActionResult> GetProfile()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        return NotFound(new { Message = "User not found" });

        //    return Ok(new
        //    {
        //        user.Id,
        //        user.FullName,
        //        user.Email,
        //        user.PhoneNumber,
        //        user.CreatedDate
        //    });
        //}

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.DateOfBirth,
                user.Address,
                user.Department,
                user.Gender,
                user.EmployeeCode,
                user.CreatedDate,
                Roles = roles
            });
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            user.DateOfBirth = dto.DateOfBirth;
            user.Address = dto.Address;
            user.Department = dto.Department;
            user.Gender = dto.Gender;
            user.EmployeeCode = dto.EmployeeCode;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Profile updated successfully" });
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            if (user.RefreshToken != dto.RefreshToken)
                return Unauthorized(new { Message = "Invalid refresh token" });

            if (user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized(new { Message = "Refresh token expired" });

            var roles = await _userManager.GetRolesAsync(user);

            var newToken = _tokenService.GenerateToken(
                user.Id,
                user.Email!,
                roles.FirstOrDefault() ?? "Employee");

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Ok(new { Message = "If email exists, reset link will be sent" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendPasswordResetEmailAsync(dto.Email, token);

            return Ok(new { Message = "Password reset email sent successfully" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Password reset successfully" });
        }

    }
}