using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Task;
using Todo.Infrastructure;

namespace Todo.Service.Commands.Task
{
    public class AddTaskCommand : IRequest<TaskDto>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Priority { get; set; }
    }

    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, TaskDto>
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddTaskCommandHandler(TodoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(AddTaskCommand command, CancellationToken cancellationToken)
        {
            var task = new Domain.Task.Task(command.UserId,
                                command.Title,
                                command.Description,
                                command.Priority);

            await _dbContext.AddAsync(task, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }
    }
}
