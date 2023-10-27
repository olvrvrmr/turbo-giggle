// TaskManagement/TaskQueueManager.cs

using System.Collections.Generic;

namespace TaskManagement
{
    public class TaskQueueManager
    {
        private readonly Queue<Task> _taskQueue;

        public TaskQueueManager()
        {
            _taskQueue = new Queue<Task>();
        }

        public void EnqueueTask(Task task)
        {
            _taskQueue.Enqueue(task);
        }

        public Task DequeueTask()
        {
            return _taskQueue.Count > 0 ? _taskQueue.Dequeue() : null;
        }

        public int QueueSize()
        {
            return _taskQueue.Count;
        }
    }
}
