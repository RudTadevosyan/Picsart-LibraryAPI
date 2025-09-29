using Library.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto> Login(LoginDto loginDto);
    Task<TokenDto> Register(RegisterDto registerDto);
}