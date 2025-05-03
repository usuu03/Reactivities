using System;
using Application.Activities.DTO;
using Application.Common;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class CreateActivity
{
    // Command class represents the request to create a new activity
    // It implements the IRequest interface from MediatR, and expects
    // a string response (the ID of the created activity)
    public class Command : IRequest<Result<string>>
    {
        // The Activity object to be created. Marked as required
        // to ensure that it is provided when creating a new activity
        public required CreateActivityDTO ActivityDto { get; set; }
    }

    // Handler class to process the Command and performs the actual activity creation
    // It implements the IRequestHandler interface from MediatR
    // and specifies that it handles the Command type and returns a string
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        // Handle Method contains the logic for processing the command
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = mapper.Map<Activity>(request.ActivityDto);
            // Add the new activity to the database context
            context.Activities.Add(activity);

            // Save the changes to the database asynchronously
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            // If the result is not successful, return a failure result with an error message
            if (!result) return Result<string>.Failure("Failed to create activity", 400);

            // Return a success result with the ID of the created activity
            return Result<string>.Success(activity.Id);
        }
    }
}
