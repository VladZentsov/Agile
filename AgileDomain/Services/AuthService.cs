using AgileDataAccess.Entities;
using AgileDataAccess.UoW;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _jwtSecret;
        private readonly int _jwtExpiryInDays;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_jwtSecret = jwtSecret;
            //_jwtExpiryInDays = jwtExpiryInDays;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = (await _unitOfWork.UserRepository.FindAsync(u => u.Email == registerDto.Email)).FirstOrDefault();
            if (existingUser != null)
                throw new ArgumentException("User already exists");

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Password = HashPassword(registerDto.Password), // Implement a hashing method
                Position = registerDto.Position
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = (await _unitOfWork.UserRepository.FindAsync(u => u.Email == loginDto.Email)).FirstOrDefault();
            if (user == null || !VerifyPassword(loginDto.Password, user.Password)) // Implement a password verification method
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateAuthResponse(user);
        }

        public async Task<bool> CheckUserPositionAsync(int userId, string position)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return user.Position.ToString().Equals(position, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> CheckUserRoleInProjectAsync(int userId, int projectId, string role)
        {
            var projectTeam = (await _unitOfWork.ProjectTeamRepository.FindAsync(pt => pt.UserID == userId && pt.ProjectID == projectId)).FirstOrDefault();
            if (projectTeam == null)
                throw new KeyNotFoundException("User is not part of the project");

            return projectTeam.Role.ToString().Equals(role, StringComparison.OrdinalIgnoreCase);
        }

        private AuthResponseDto GenerateAuthResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Position.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtExpiryInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                FullName = user.FullName,
                Position = user.Position.ToString()
            };
        }

        private string HashPassword(string password)
        {
            // Implement a password hashing method (e.g., BCrypt or PBKDF2)
            return password;
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Implement a password verification method (e.g., BCrypt or PBKDF2)
            return enteredPassword == storedHash;
        }
    }
}
