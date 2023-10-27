using TM = TaskManagement;
using SL = SchedulerLib;

namespace SchedulerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            TM.TaskQueueManager taskQueueManager = new TM.TaskQueueManager();
            TM.Task task1 = new TM.Task(1, "Task1");
            TM.Task task2 = new TM.Task(2, "Task2");
            taskQueueManager.EnqueueTask(task1);
            taskQueueManager.EnqueueTask(task2);

            SL.Scheduler scheduler = new SL.Scheduler(taskQueueManager);
            scheduler.Start(TimeSpan.FromSeconds(2));

            // Keep the console app running
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }
    }
}