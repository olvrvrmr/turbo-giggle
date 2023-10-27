using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManagement;

namespace SchedulerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulerController : ControllerBase
    {
        private readonly TaskQueueManager _taskQueueManager;

        public SchedulerController(TaskQueueManager taskQueueManager)
        {
            _taskQueueManager = taskQueueManager;
        }

        [HttpGet("tasks")]
        public ActionResult<IEnumerable<TaskManagement.Task>> GetTasks()
        {
            return Ok(_taskQueueManager.GetTasks());
        }

        [HttpPost("tasks")]
        public ActionResult<TaskManagement.Task> AddTask([FromBody] TaskManagement.Task newTask)
        {
            _taskQueueManager.EnqueueTask(newTask);
            return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
        }

        // ... additional actions for other scheduling functionalities
    }
}
