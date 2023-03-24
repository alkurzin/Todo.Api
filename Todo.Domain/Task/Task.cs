namespace Todo.Domain.Task
{
    public class Task
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public long Priority { get; set; }

        public Task()
        {

        }

        public Task(string userId, string title,
            string description, long priority)
        {
            UserId = userId;
            Title = title;
            Description = description;
            IsCompleted = false;
            Priority = priority;
        }

        public void Update(string userId, string title,
            string description, long priority)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Priority = priority;
        }

        public void Completed()
        {
            IsCompleted = true;
        }

        public void NotCompleted()
        {
            IsCompleted = false;
        }
    }
}
