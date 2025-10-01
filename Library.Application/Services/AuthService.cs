using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Application.Interfaces;
using Library.Shared.DTOs.Auth;
using Library.Domain.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly SignInManager<IdentityUser<int>> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<IdentityUser<int>> userManager, 
        SignInManager<IdentityUser<int>> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email)
            ?? throw new NotFoundException("User Not Found");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            throw new DomainException("Invalid Credentials");

        return await GenerateToken(user);
    }

    public async Task<TokenDto> Register(RegisterDto registerDto)
    {
        var user = new IdentityUser<int>
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            throw new DomainException("Registration Failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        // Normalize role
        var role = registerDto.Role.Trim();
        if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            role = "Admin";
        else if (role.Equals("Member", StringComparison.OrdinalIgnoreCase))
            role = "Member";
        else
            throw new DomainException("Invalid role. Must be 'Admin' or 'Member'.");


        await _userManager.AddToRoleAsync(user, role);
        return await GenerateToken(user);
    }

    private async Task<TokenDto> GenerateToken(IdentityUser<int> user)
    {
        var claims = new List<Claim>
        {
            //Sub - the unique id for the user
            //Jti - unique id for the actual JWT token => used for, to track, to revoke or prevent from being reused
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        
        //adding to claims list all roles that user has assigned
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new TokenDto { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}