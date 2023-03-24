using Todo.Infrastructure;

namespace TaskManager.UnitTests
{
    public class TestUtils
    {
        private readonly TodoDbContext _dbContext;

        public TodoDbContext DbContext => _dbContext;

        public TestUtils()
        {
            InitDbContext();
        }

        private void InitDbContext()
        {
            //var builder = new DbContextOptionsBuilder<TaskManagerDbContext>();
            //builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }
    }
}
