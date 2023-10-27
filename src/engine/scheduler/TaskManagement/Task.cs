// TaskManagement/Task.cs

namespace TaskManagement
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public int Retries { get; set; }
        public int MaxRetries { get; set; }
        
        public Task(int id, string name)
        {
            Id = id;
            Name = name;
            State = "Pending";
            Retries = 0;
            MaxRetries = 3;  // Default maximum retries
        }
    }
}
