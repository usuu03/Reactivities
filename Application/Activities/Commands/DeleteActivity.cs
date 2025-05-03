using System;
using Application.Common;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Find the activity in the database by its ID.
            // If the activity is not found, throw an exception.
            var activity = await context.Activities
                .FindAsync([request.Id], cancellationToken);

            if (activity == null) return Result<Unit>.Failure("Activity not found", 404);

            // Remove the activity from the database context.
            context.Remove(activity);

            // Save the changes to the database asynchronously.
            // The cancellationToken allows the operation to be canceled if needed.
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            // If the result is not successful, return a failure result with an error message.
            if (!result) return Result<Unit>.Failure("Failed to delete activity", 400);

            return Result<Unit>.Success(Unit.Value);

        }
    }

}
