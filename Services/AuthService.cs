using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PagebaTask.Entities;

namespace PagebaTask.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = username
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }

            throw new Exception("Unable to register user");
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            throw new Exception("Invalid username or password");
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
{
    var claims = new List<Claim>();
    if (user != null)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));

        var roles = await _userManager.GetRolesAsync(user);
        if (roles != null)
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role ?? ""));
            }
        }
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["Jwt:ExpireDays"] ?? "0"));

    var token = new JwtSecurityToken(
        _config["Jwt:Issuer"] ?? "",
        _config["Jwt:Issuer"] ?? "",
        claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

    }
}