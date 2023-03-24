using System.ComponentModel;

namespace Todo.Domain.Task
{
    public class TaskDto
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public long Priority { get; set; }
    }
}
