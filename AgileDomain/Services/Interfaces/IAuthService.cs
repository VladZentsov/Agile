using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<bool> CheckUserPositionAsync(int userId, string position);
        Task<bool> CheckUserRoleInProjectAsync(int userId, int projectId, string role);
    }
}
