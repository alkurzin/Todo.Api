using AutoMapper;

namespace Todo.Domain.Task.MapperProfiles
{
    public class TaskMapperProfile : Profile
    {
        public TaskMapperProfile()
        {
            CreateMap<Task, TaskDto>();
        }
    }
}
