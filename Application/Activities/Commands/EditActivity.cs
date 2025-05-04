using System;
using Application.Activities.DTOs;
using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{

    // Command class represents the request to edit an existing activity
    // It implements the IRequest interface from MediatR
    public class Command : IRequest<Result<Unit>>
    {

        public required EditActivityDto ActivityDto { get; set; }
    }

    // Handler class to process the Command and performs the actual activity editing
    // It implements the IRequestHandler interface from MediatR
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        // The Handle method contains the logic for processing the Command.
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Find the activity in the database by its ID.
            // If the activity is not found, throw an exception.
            var activity = await context.Activities
                .FindAsync([request.ActivityDto.Id], cancellationToken);

            if (activity == null) return Result<Unit>.Failure("Activity not found", 404);

            // The mapper is used to map the properties from the request to the existing activity.
            mapper.Map(request.ActivityDto, activity);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            // If the result is not successful, return a failure result with an error message.
            if (!result) return Result<Unit>.Failure("Failed to update activity", 400);

            // Save the changes to the database asynchronously.
            // The cancellationToken allows the operation to be canceled if needed.
            return Result<Unit>.Success(Unit.Value);

        }

    }


}
