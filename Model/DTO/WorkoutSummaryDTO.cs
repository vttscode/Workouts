namespace Workouts.Model.DTO
{
    public class WorkoutSummaryDTO
    {
        public int WorkoutId { get; set; }
        public int TotalSets { get; set; }
        public int TotalReps { get; set; }
        public string TotalDuration { get; set; }
    }
}
