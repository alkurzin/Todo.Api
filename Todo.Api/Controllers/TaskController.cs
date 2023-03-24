using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly IMediator _mediator;
        private readonly TodoDbContext _dbContext;

        public TaskController(IMediator mediator, TodoDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
    }
}
