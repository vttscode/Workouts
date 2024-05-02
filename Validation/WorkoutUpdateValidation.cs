using FluentValidation;
using Workouts.Model.DTO;

namespace Workouts.Validation
{
    
     public class WorkoutUpdateValidation : AbstractValidator<WorkoutUpdateDTO>
        {
            public WorkoutUpdateValidation()
            {
                RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
                RuleFor(model => model.Title).NotEmpty();
                RuleFor(model => model.Description).NotEmpty();
                RuleFor(model => model.Exercises).NotEmpty();

            }


        }
    
}
