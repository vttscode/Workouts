using Microsoft.AspNetCore.Mvc;
using Workouts.WorkoutStore;
using Workouts.Model;
using Workouts.Model.DTO;
using Workouts;
using AutoMapper;
using FluentValidation;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/getWorkouts", () => {
    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
    response.Result = WorkoutStore.workoutList;
    if (response.Result == null)
    {
        response.ErrorMessages.Add("Error");
        return Results.BadRequest(response);
    }
    else
    {
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
}).WithName("GetWorkouts").Produces<APIResponse>(200).Produces(400);

app.MapGet("/api/getWorkout/{id}", (int id) => {
    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
    response.Result = WorkoutStore.workoutList.FirstOrDefault(u => u.Id == id);
    if (response.Result == null)
    {
        response.ErrorMessages.Add("Invalid Id");
        return Results.BadRequest(response);
    }
    else
    {
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
    
    }).WithName("GetWorkout").Produces<APIResponse>(200).Produces(400);

app.MapPost("/api/createWorkout", async (IMapper _mapper, IValidator<WorkoutCreateDTO> _validation, [FromBody] WorkoutCreateDTO workoutCreateDTO) => {
    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };   

    var validationResult = await _validation.ValidateAsync(workoutCreateDTO);
    if (!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    Workout workout = _mapper.Map <Workout> (workoutCreateDTO);

    workout.Id = WorkoutStore.workoutList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
    WorkoutStore.workoutList.Add(workout);
    WorkoutDTO workoutDTO = _mapper.Map<WorkoutDTO>(workout);
    
    response.Result = workoutDTO;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.Created;
    return Results.Ok(response);
}).WithName("CreateWorkout").Accepts<WorkoutCreateDTO>("application/json").Produces<APIResponse>(201).Produces(400);

app.MapPut("/api/updateWorkout", async (IMapper _mapper,
    IValidator<WorkoutUpdateDTO> _validation, [FromBody] WorkoutUpdateDTO workoutUpdateDTO) => {
        APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
        var validationResult = await _validation.ValidateAsync(workoutUpdateDTO);
        if (!validationResult.IsValid)
        {
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
            return Results.BadRequest(response);
        }
        Workout workoutfromStore = WorkoutStore.workoutList.FirstOrDefault(u => u.Id == workoutUpdateDTO.Id);

        workoutfromStore.Title = workoutUpdateDTO.Title;
        workoutfromStore.Description = workoutUpdateDTO.Description;
        workoutfromStore.Exercises = workoutUpdateDTO.Exercises;

        response.Result = _mapper.Map<WorkoutDTO>(workoutfromStore); ;
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }).WithName("UpdateWorkout")
    .Accepts<WorkoutUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400);

app.MapDelete("/api/deleteWorkout/{id}", (int id) => {
    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
    Workout workoutFromStore = WorkoutStore.workoutList.FirstOrDefault(u => u.Id == id);
    if (workoutFromStore != null)
    {
        WorkoutStore.workoutList.Remove(workoutFromStore);
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.NoContent;
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("Invalid Id");
        return Results.BadRequest(response);
    }
});

app.MapGet("/api/getWorkoutSummary/{id}", (int id) => {

    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
    Workout workoutFomStore = WorkoutStore.workoutList.FirstOrDefault(u => u.Id == id);
    if (workoutFomStore == null)
    {
        response.ErrorMessages.Add("Invalid Id");
        return Results.BadRequest(response);
    }
    else
    {
        int totalSumSets = workoutFomStore.Exercises.Sum(u => u.Sets);
        int totalSumReps = workoutFomStore.Exercises.Sum(u => u.Reps);
        List<string> durations = workoutFomStore.Exercises.Select(u => u.Duration).ToList();
        int totalMinutes = 0;
        foreach (string duration in durations)
        {
            int minutes = int.Parse(duration.Split(' ')[0]);
            totalMinutes += minutes;
        }

        WorkoutSummaryDTO summaryDTO = new WorkoutSummaryDTO
        {
            //WorkoutId = WorkoutSummaryStore.workoutSummaryList.OrderByDescending(u => u.WorkoutId).FirstOrDefault().WorkoutId + 1,
            WorkoutId = id,
            TotalSets = totalSumSets,
            TotalReps = totalSumReps,
            TotalDuration = totalMinutes + " minutes"

        };

        //WorkoutSummaryStore.workoutSummaryList.Add((IEnumerable<WorkoutSummary>)summaryDTO);

        response.Result = summaryDTO;
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
}).WithName("GetWorkoutSummary").Produces<APIResponse>(200);

app.Run();

