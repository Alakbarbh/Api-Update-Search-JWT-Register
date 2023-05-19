using Services.DTOs.Account;
using Services.Helpers.Responses;

namespace Services.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponse> SignUpAsync(RegisterDto model);
    }
}
