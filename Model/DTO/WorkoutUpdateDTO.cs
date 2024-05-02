namespace Workouts.Model.DTO
{
    public class WorkoutUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
