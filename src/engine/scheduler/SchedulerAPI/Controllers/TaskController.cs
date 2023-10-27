using Microsoft.AspNetCore.Mvc;
using SchedulerLib;
using TaskManagement;
using System.Collections.Generic;

namespace SchedulerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskQueueManager _taskQueueManager;

        public TasksController(TaskQueueManager taskQueueManager)
        {
            _taskQueueManager = taskQueueManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskManagement.Task>> GetTasks()
        {
            return Ok(_taskQueueManager.GetTasks());
        }

        [HttpPost]
        public ActionResult<TaskManagement.Task> AddTask(TaskManagement.Task newTask)
        {
            _taskQueueManager.EnqueueTask(newTask);
            return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
        }

        // ... other methods for updating and deleting tasks
    }
}
