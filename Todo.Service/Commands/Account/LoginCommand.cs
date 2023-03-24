using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.User;
using Todo.Service.Infrastructure.Account;

namespace Todo.Service.Commands.Account
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly ILoginServices _loginServices;

        public LoginCommandHandler(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        public async Task<LoginDto> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _loginServices.Login(command, cancellationToken);
        }
    }
}
