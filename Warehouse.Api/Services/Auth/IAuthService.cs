using Warehouse.Api.DTOs.Requests;
using Warehouse.Api.DTOs.Responses;

namespace Warehouse.Api.Services.Auth;

public interface IAuthService
{
    Task<RegisterResponse?> RegisterAsync(RegisterRequest request);
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}