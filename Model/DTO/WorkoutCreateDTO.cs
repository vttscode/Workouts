namespace Workouts.Model.DTO
{
    public class WorkoutCreateDTO
    {       
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
