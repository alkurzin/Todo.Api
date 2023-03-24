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
        public long Hours { get; set; }
        public long Minutes { get; set; }
        [DefaultValue("DD.MM.YYYY")]
        public string Date { get; set; }

        [DefaultValue("00:00")]
        public string Time { get; set; }
    }
}
