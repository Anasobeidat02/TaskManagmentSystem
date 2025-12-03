using Domains.DTOs.TaskItem;
using Domains.Entities;
using Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskItem _taskItemService;

        public TaskItemsController(ITaskItem taskItemService)
        {
            _taskItemService = taskItemService;
        }


        [HttpGet]
        [Route("get-all")] 
        public async Task<IActionResult> GetAllTaskItems()
        {
            var taskItems = await _taskItemService.GetAll();
            return Ok(taskItems);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTaskItem(AddItemRequet request)
        {
            if (request == null)
                return BadRequest("Task item cannot be null.");

             var data = User.Claims.FirstOrDefault(); 
            var userIdFromToken = data != null ? int.Parse(data.Value) : 0;
                
            await _taskItemService.Add(request ,userIdFromToken);
            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            await _taskItemService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetTaskItemById(int id)
        {
            var taskItem = await _taskItemService.GetById(id);
            if (taskItem == null)
                return NotFound();

            return Ok(taskItem);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateTaskItem(UpdateItemRequest request)
        {
            if (request == null)
                return BadRequest("Update data cannot be null.");

            await _taskItemService.Update(request);
            return Ok();
        }

    }
}
