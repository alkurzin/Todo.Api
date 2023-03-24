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
    public class UpdateTaskCommand : IRequest<TaskDto>
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Priority { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(TodoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.Where(t => t.Id == command.TaskId).FirstOrDefaultAsync(cancellationToken);

            task.Update(command.Title, command.Description, command.Priority);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }
    }
}
