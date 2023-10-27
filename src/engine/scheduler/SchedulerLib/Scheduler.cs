using System;
using System.Threading;
using TM = TaskManagement;

namespace SchedulerLib;
public class Scheduler
{
    private Timer _timer;
    private TM.TaskQueueManager _taskQueueManager;

    public Scheduler(TM.TaskQueueManager taskQueueManager)
    {
        _taskQueueManager = taskQueueManager;
    }

    public void Start(TimeSpan interval)
    {
        _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, interval);
    }

    private void ExecuteTask(object state)
    {
        TM.Task task = _taskQueueManager.DequeueTask();
        if(task != null)
        {
            task.State = TM.TaskState.InProgress;
            System.Console.WriteLine($"Executing Task: {task.Name}");
            Thread.Sleep(1000);

            task.State = TM.TaskState.Completed;
            System.Console.WriteLine($"Task {task.Name} completed.");
        }
    }
}
