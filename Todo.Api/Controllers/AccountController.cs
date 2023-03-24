using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Domain.User;
using Todo.Service.Commands.Account;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///Регистрация
        /// </summary>
        [HttpPost("Register")]
        public async Task<LoginDto> Register([FromBody] RegistrerCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Вход
        /// </summary>
        [HttpPost("Login")]
        public async Task<LoginDto> Login([FromBody] LoginCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
