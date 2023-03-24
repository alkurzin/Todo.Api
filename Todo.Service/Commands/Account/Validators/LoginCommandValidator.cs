using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Infrastructure;

namespace Todo.Service.Commands.Account.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly AuthDbContext _authDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginCommandValidator(AuthDbContext authDbContext, SignInManager<IdentityUser> signInManager)
        {
            _authDbContext = authDbContext;
            _signInManager = signInManager;

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email пуст");
            RuleFor(c => c.Email).Must(IsFoundEmail).WithMessage("Пользователь не найден");

            RuleFor(c => c.Password).NotEmpty().WithMessage("Введите пароль");
            RuleFor(c => c.Password).MustAsync(IsLogin).WithMessage("Пароль не верный");
        }

        private async Task<bool> IsLogin(LoginCommand login, string password, CancellationToken cancellationToken)
        {
            if (login.Email != null && login.Password != null)
            {
                if(login.Email.Length > 0 && login.Password.Length > 0)
                {
                    var result = await _signInManager.PasswordSignInAsync(login.Email, password, false, false);
                    return result.Succeeded;
                }
            }

            return true;
        }

        private bool IsFoundEmail(string email)
        {
            if(email != null)
            {
                if (email.Length > 0)
                {
                    return _authDbContext.Users.Select(u => u.Email).Contains(email);
                }
            }

            return true;
        }
    }
}
