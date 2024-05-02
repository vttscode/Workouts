using Workouts.Model;

namespace Workouts.WorkoutStore
{
    public class WorkoutSummaryStore
    {
        public static List<WorkoutSummary> workoutSummaryList = new List<WorkoutSummary>
        {
                //Fake data
                new WorkoutSummary
                {
                    WorkoutId = 1,
                    TotalSets = 6,
                    TotalReps = 18,
                    TotalDuration = "25 minutes"
                },
        };
    }
}


