using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Todo.Domain.User;
using Todo.Infrastructure;
using Todo.Service.Infrastructure.Account;

namespace Todo.Service.Commands.Account
{
    public class RegistrerCommand : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class RegistrerCommandHandler : IRequestHandler<RegistrerCommand, LoginDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TodoDbContext _dbContext;
        private readonly ILoginServices _loginServices;


        public RegistrerCommandHandler(UserManager<IdentityUser> userManager,
                                        SignInManager<IdentityUser> signInManager,
                                        TodoDbContext dbContext,
                                        ILoginServices loginServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _loginServices = loginServices;
        }

        public async Task<LoginDto> Handle(RegistrerCommand command, CancellationToken cancellationToken)
        {
            var identityUser = new IdentityUser
            {
                Email = command.Email,
                PasswordHash = command.Password,
                UserName = command.Email
            };

            var result = await _userManager.CreateAsync(identityUser, command.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(identityUser, false);
            }
            else
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw new HttpResponseException(responseMessage);
            }

            var loginCommand = new LoginCommand { Email = command.Email, Password = command.Password, RememberMe = true };

            return await _loginServices.Login(loginCommand, cancellationToken);
        }
    }
}
