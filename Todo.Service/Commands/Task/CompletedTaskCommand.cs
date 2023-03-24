using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Task;
using Todo.Infrastructure;

namespace Todo.Service.Commands.Task
{
    public class CompletedTaskCommand : IRequest<TaskDto>
    {
        public long Id { get; set; }
    }
    public class CompletedTaskCommandHandler : IRequestHandler<CompletedTaskCommand, TaskDto>
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompletedTaskCommandHandler(TodoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(CompletedTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.Where(t => t.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            task.Completed();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }
    }
}
