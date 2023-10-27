using T = TaskManagement;

namespace SchedulerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the TaskQueueManager
            T.TaskQueueManager taskQueueManager = new T.TaskQueueManager();

            // Create some tasks
            T.Task task1 = new T.Task(1, "Task1");
            T.Task task2 = new T.Task(2, "Task2");

            // Enqueue tasks
            taskQueueManager.EnqueueTask(task1);
            taskQueueManager.EnqueueTask(task2);

            // Display the current queue size
            Console.WriteLine($"Queue Size: {taskQueueManager.QueueSize()}");

            // Dequeue and display a task
            T.Task nextTask = taskQueueManager.DequeueTask();

            if (nextTask != null)
            {
                Console.WriteLine($"Dequeued Task: {nextTask.Name}, State: {nextTask.State}");
            }

            // Display the remaining queue size
            Console.WriteLine($"Queue Size after Dequeue: {taskQueueManager.QueueSize()}");
        }
    }
}