using FluentValidation;
using Workouts.Model.DTO;

namespace Workouts.Validation
{
    public class WorkoutCreateValidation : AbstractValidator<WorkoutCreateDTO>
    {
        public WorkoutCreateValidation()
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.Exercises).NotEmpty();
        }
    }
}
