using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Task;
using Todo.Infrastructure;

namespace Todo.Service.Queries.Tasks
{
    public class GetTasksQuery : IRequest<IEnumerable<TaskDto>>
    {
        public string UserId { get; set; }
        public string SearchString { get; set; }
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(TodoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery command, CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<TaskDto>(_dbContext.Tasks.Where(t => t.UserId == command.UserId && t.Title.Contains(command.SearchString))).ToListAsync(cancellationToken);
        }
    }
}
