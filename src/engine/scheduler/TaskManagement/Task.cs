// TaskManagement/Task.cs

namespace TaskManagement
{
    public enum TaskState { Pending, InProgress, Completed, Failed }
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskState State{ get; set; }
        public int Retries { get; set; }
        public int MaxRetries { get; set; }

        public Task(int id, string name)
        {
            Id = id;
            Name = name;
            State = TaskState.Pending;
            Retries = 0;
            MaxRetries = 3;  // Default maximum retries
        }
    }
}
