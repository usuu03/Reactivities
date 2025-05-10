using System;
using Application.Activities.Commands;
using Application.Activities.DTO;
using Application.Activities.DTOs;
using Application.Activities.Queries;
using Application.Common;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

// Takes AppDbContext as a dependency to access the database
// Inherits from BaseApiController which is a base class for all API controllers
public class ActivitiesController : BaseApiController
{
    [HttpGet]
    // Method that returns a list of activities
    public async Task<ActionResult<List<Activity>>> GetActivties()
    {
        // Fetches all activities from the database asynchronously
        // and returns them as a list
        return await Mediator.Send(new GetActivityList.Query());

    }

    [Authorize]
    [HttpGet("{id}")]
    // Method that returns the details of a specific activity by its ID
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetActivityDetails.Query { Id = id }));

    }

    [HttpPost]
    // Method that creates a new activity
    public async Task<ActionResult<string>> CreateActivity(CreateActivityDTO activityDto)
    {
        // Sends the command to create a new activity and returns the result
        return HandleResult(await Mediator.Send(new CreateActivity.Command { ActivityDto = activityDto }));
    }

    [HttpPut]
    // Method that updates an existing activity by its ID
    public async Task<ActionResult> EditActivity(EditActivityDto activity)
    {
        return HandleResult(await Mediator.Send(new EditActivity.Command { ActivityDto = activity }));

    }

    [HttpDelete("{id}")]
    // Method that deletes an existing activity by its ID
    public async Task<ActionResult> DeleteActivity(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteActivity.Command { Id = id }));

    }
}