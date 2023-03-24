using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Infrastructure;

namespace Todo.Service.Commands.Task
{
    public class DeleteTaskCommand : IRequest
    {
        public long Id { get; set; }
    }
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly TodoDbContext _dbContext;

        public DeleteTaskCommandHandler(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Unit> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.Where(t => t.Id == command.Id).FirstOrDefaultAsync(cancellationToken);

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Unit();
        }
    }
}
