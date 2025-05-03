using System;
using Application.Activities.DTO;
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
    public class Command : IRequest<string>
    {
        // The Activity object to be created. Marked as required
        // to ensure that it is provided when creating a new activity
        public required CreateActivityDTO ActivityDto { get; set; }
    }

    // Handler class to process the Command and performs the actual activity creation
    // It implements the IRequestHandler interface from MediatR
    // and specifies that it handles the Command type and returns a string
    public class Handler(AppDbContext context, IMapper mapper, IValidator<Command> validator) : IRequestHandler<Command, string>
    {
        // Handle Method contains the logic for processing the command
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request);
            var activity = mapper.Map<Activity>(request.ActivityDto);
            // Add the new activity to the database context
            context.Activities.Add(activity);

            // Save changes to the database
            // CancellationToken is used to cancel the operation if needed
            await context.SaveChangesAsync(cancellationToken);

            // Return the ID of the newly created activity
            return activity.Id;
        }
    }
}
