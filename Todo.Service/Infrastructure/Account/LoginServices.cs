using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.User;
using Todo.Service.Commands.Account;

namespace Todo.Service.Infrastructure.Account
{
    public interface ILoginServices
    {
        public Task<LoginDto> Login(LoginCommand command, CancellationToken cancellationToken);
    }

    public class LoginServices : ILoginServices
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginServices(SignInManager<IdentityUser> signInManager, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task<LoginDto> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = new LoginDto();
            var result = await _signInManager.PasswordSignInAsync(command.Email, command.Password, command.RememberMe, false);
            if (result.Succeeded)
            {
                string tokenString = BuildToken();
                user.Id = _userManager.FindByEmailAsync(command.Email).Result.Id;
                user.Token = tokenString;
                user.Email = command.Email;

                return user;
            }
            else
            {
                return null;
            }
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JwtToken:Issuer"],
              _config["JwtToken:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
