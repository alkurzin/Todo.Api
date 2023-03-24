using FluentValidation;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Todo.Infrastructure;

namespace Todo.Service.Commands.Account.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegistrerCommand>
    {
        private readonly AuthDbContext _authDbContext;

        public RegisterCommandValidator(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email пуст");
            RuleFor(c => c.Email).Must(IsFoundEmail).WithMessage("Почта занята");
            RuleFor(c => c.Email).Must(IsValidEmail).WithMessage("Не корректный формат почты");


            RuleFor(c => c.Password).NotEmpty().WithMessage("Введите пароль");
            RuleFor(c => c.Password).Must(IsValidPasswordLength).WithMessage("Минимальная длина - 6 символов");
            RuleFor(c => c.Password).Must(IsValidPasswordUppercase).WithMessage("Tребуются символы в верхнем регистре");
            RuleFor(c => c.Password).Must(IsValidPasswordLowercase).WithMessage("Tребуются символы в нижнем регистре");
            RuleFor(c => c.Password).Must(IsValidPasswordDigit).WithMessage("Tребуются цифры");

            RuleFor(c => c.PasswordConfirm).NotEmpty().WithMessage("Введите подтверждение пароля");
            RuleFor(c => c.PasswordConfirm).Must(IsPasswordConfirm).WithMessage("Пароли не совпадают");
        }

        private bool IsPasswordConfirm(RegistrerCommand registrer, string password)
        {
            if (registrer.Password == null || password == null)
            {
                return true;
            }

            return registrer.Password == password;
        }
        private bool IsValidPasswordLength(string password)
        {
            if (password == null)
            {
                return true;
            }

            var hasMinimum6Chars = new Regex(@".{6,}");

            return  hasMinimum6Chars.IsMatch(password);
        }

        private bool IsValidPasswordUppercase(string password)
        {
            if (password == null)
            {
                return true;
            }

            var hasUpperChar = new Regex(@"[A-Z]+");

            return hasUpperChar.IsMatch(password);
        }

        private bool IsValidPasswordLowercase(string password)
        {
            if (password == null)
            {
                return true;
            }

            var hasLowerChar = new Regex(@"[a-z]+");

            return hasLowerChar.IsMatch(password);
        }

        private bool IsValidPasswordDigit(string password)
        {
            if (password == null)
            {
                return true;
            }

            var hasNumber = new Regex(@"[0-9]+");

            return hasNumber.IsMatch(password);
        }

        private bool IsFoundEmail(string email)
        {
            return  !_authDbContext.Users.Select(u => u.Email).Contains(email);
        }

        public bool IsValidEmail(string emailaddress)
        {
            if(emailaddress == null)
            {
                return true;
            }

            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}