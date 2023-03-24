using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Task;
using Todo.Infrastructure;

namespace Todo.Service.Queries.Tasks
{
    public class GetTaskQuery : IRequest<TaskDto>
    {
        public long Id { get; set; }
    }

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskQueryHandler(TodoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(GetTaskQuery command, CancellationToken cancellationToken)
        {
            return _mapper.Map<TaskDto>(await _dbContext.Tasks.Where(t => t.Id == command.Id).FirstOrDefaultAsync(cancellationToken));
        }
    }
}
