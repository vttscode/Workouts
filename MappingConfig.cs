using AutoMapper;
using Workouts.Model;
using Workouts.Model.DTO;

namespace Workouts
{
    public class MappingConfig :Profile
    {
        public MappingConfig() 
        {
            CreateMap<Workout, WorkoutCreateDTO>().ReverseMap();
            CreateMap<Workout, WorkoutDTO>().ReverseMap();
            CreateMap<Workout, WorkoutUpdateDTO>().ReverseMap();
        }


    }
}
