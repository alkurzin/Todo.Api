using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Task;
using Todo.Service.Commands.Task;
using Todo.Service.Queries.Tasks;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///Добавить задачу
        /// </summary>
        [HttpPost("")]
        public async Task<TaskDto> AddTask([FromBody] AddTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Получить задачи
        /// </summary>
        [HttpGet("")]
        public async Task<IEnumerable<TaskDto>> GetTasks([FromQuery] GetTasksQuery command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Получить задачу
        /// </summary>
        [HttpGet("Task")]
        public async Task<TaskDto> GetTask([FromQuery] GetTaskQuery command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Редактировать задачу
        /// </summary>
        [HttpPut("")]
        public async Task<TaskDto> Update([FromBody] UpdateTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Удалить задачу
        /// </summary>
        [HttpDelete("")]
        public async Task<Unit> Delete([FromQuery] DeleteTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Пометить задачу как выполненая
        /// </summary>
        [HttpPut("Completed")]
        public async Task<TaskDto> Completed([FromQuery] CompletedTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        ///Пометить задачу как не выплоненая
        /// </summary>
        [HttpPut("NotCompleted")]
        public async Task<TaskDto> NotCompleted([FromQuery] NotCompletedTaskCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
