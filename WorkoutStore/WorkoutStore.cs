using Workouts.Model;

namespace Workouts.WorkoutStore
{
    public class WorkoutStore
    {     
        public static List<Workout> workoutList = new List<Workout>
        {
            // Fake data
            new Workout
            {
                Id = 1,
                Title = "Upper Body Workout",
                Description = "Focuses on chest, shoulders, back, and arms.",
                Exercises = new List<Exercise>
                {
                    new Exercise { Name = "Push-ups", Sets = 3, Reps = 10, Duration = "10 minutes" },
                    new Exercise { Name = "Pull-ups", Sets = 3, Reps = 8, Duration = "15 minutes" }
                }
            }          

        };       
    }
}